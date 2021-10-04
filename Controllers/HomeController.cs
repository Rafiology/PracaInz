using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ZgloszeniaIT.Models;
using ZgloszeniaIT.Services;

namespace ZgloszeniaIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IApplicationRepository applicationRepository, ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,Operator")]
        public IActionResult List()
        {
            var list = _applicationRepository.GetReports(null, null, false);
            return View(list);
        }
        
        [Authorize(Roles = "Administrator,Operator")]
        public IActionResult AssignedList()
        {
            var list = _applicationRepository.GetReports(null, User.Identity.Name, false);
            return View(list);
        }
        
        [Authorize(Roles = "Administrator,Operator")]
        public IActionResult ClosedList()
        {
            var list = _applicationRepository.GetReports(null, null, true);
            return View(list);
        }

        [Authorize]
        public IActionResult MyList()
        {
            var list = _applicationRepository.GetReports(User.Identity.Name, null, false);
            return View(list);
        }

        [Authorize]
        public IActionResult AddReport()
        {
            ReportViewModel reportDTO = new ReportViewModel();
            reportDTO.CategoryList = _applicationRepository.GetCategory();
            reportDTO.PriorityList = _applicationRepository.GetPriority();

            return View(reportDTO);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReport([Bind("Title,Description,Priority,Category,File")] ReportViewModel reportDTO)
        {
            if (ModelState.IsValid)
            {
                reportDTO.IdReport = Guid.NewGuid();
                if (reportDTO.File != null)
                {
                    string rootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(reportDTO.File.FileName);
                    string ext = Path.GetExtension(reportDTO.File.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmss") + ext;
                    string path = Path.Combine(rootPath + "/files/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await reportDTO.File.CopyToAsync(fileStream);
                    }
                    reportDTO.FilePath = fileName;
                }
                var id = reportDTO.IdReport;
                reportDTO.Status = "CDF4DEF9-2D48-49DB-BEA4-3E69AC6D158F";
                reportDTO.User = User.Identity.Name;
                reportDTO.AddDate = DateTime.Now;

                _applicationRepository.AddReportDTO(reportDTO);
                return RedirectToAction("Details", "Home", new { id });
            }
            reportDTO.CategoryList = _applicationRepository.GetCategory();
            reportDTO.PriorityList = _applicationRepository.GetPriority();
            return View(reportDTO);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddComment(IFormCollection formCollection)
        {
            var id = Guid.Parse(formCollection["IdReport"]);
            if (formCollection["Comment"] == "") 
            {
                TempData["message"] = "Wprowadź komentarz!";
                return RedirectToAction("Details", "Home", new { id});
            }
            else
            {
                Comment comment = new Comment();
                comment.IdReport = Guid.Parse(formCollection["IdReport"]);
                comment.ComContent = formCollection["Comment"];
                comment.IdComment = Guid.NewGuid();
                comment.AddDate = DateTime.Now;
                id = comment.IdReport;
                _applicationRepository.AddComment(comment, User.Identity.Name);
                return RedirectToAction("Details", "Home", new { id });
            }
        }

        [Authorize]
        public IActionResult Details(Guid id)
        {
            var data = _applicationRepository.GetReport(id);

            return View(data);
        }

        [Authorize(Roles = "Administrator,Operator")]
        public IActionResult EditReport(Guid id)
        {
            var data = _applicationRepository.GetReport(id);
            data.CategoryList = _applicationRepository.GetCategory();
            data.PriorityList = _applicationRepository.GetPriority();

            return View(data);
        }

        [Authorize(Roles = "Administrator,Operator")]
        [HttpPost]
        public IActionResult EditReport([Bind("IdReport,User,Operator,Title,Description,Priority,Category,Status,AddDate")] ReportViewModel reportDTO)
        {
            var id = reportDTO.IdReport;
            reportDTO.Operator = User.Identity.Name;
            _applicationRepository.UpdateReportDTO(reportDTO);
            if(User.IsInRole("Operator") || User.IsInRole("Administrator"))
            {
                return RedirectToAction("Details","Operator", new { id });
            }
            else
            {
                return RedirectToAction("Details","Home", new { id });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
