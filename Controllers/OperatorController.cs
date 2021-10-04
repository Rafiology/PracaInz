using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZgloszeniaIT.Models;
using ZgloszeniaIT.Services;

namespace ZgloszeniaIT.Controllers
{
    [Authorize(Roles = "Operator, Administrator")]
    public class OperatorController : Controller
    {
        private readonly ILogger<OperatorController> _logger;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserRepository _userRepository;

        public OperatorController(IApplicationRepository applicationRepository, IUserRepository userRepository, ILogger<OperatorController> logger)
        {
            _logger = logger;
            _applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(Guid id)
        {
            var data = _applicationRepository.GetReport(id);
            return View(data);
        }

        public IActionResult AssignReport(Guid id)
        {
            _applicationRepository.AssignReport(id,User.Identity.Name);
            return RedirectToAction("Details", "Operator", new { id });
        }

        public IActionResult CloseReport(Guid id)
        {
            ReportViewModel report = new ReportViewModel();
            report.IdReport = id;
            return View(report);
        }
        
        [HttpPost]
        public IActionResult CloseReport(IFormCollection formCollection)
        {
            Guid id = Guid.Parse(formCollection["IdReport"]);
            string solution = formCollection["solution"];
            if (solution == "")
            {
                TempData["message"] = "Wprowadź powód zamknięcia zgłoszenia!";
                return RedirectToAction("CloseReport", "Operator", new { id });
            }
            else
            {
                _applicationRepository.CloseReport(id,User.Identity.Name, solution);
                return RedirectToAction("Details", "Operator", new { id });
            }
        }

        [HttpPost]
        public IActionResult AddComment(IFormCollection formCollection)
        {
            Comment comment = new Comment();
            comment.IdReport = Guid.Parse(formCollection["IdReport"]);
            comment.ComContent = formCollection["Comment"];
            comment.IdComment = Guid.NewGuid();
            comment.AddDate = DateTime.Now;
            var id = comment.IdReport;
            _applicationRepository.AddComment(comment, User.Identity.Name);
            return RedirectToAction("Details", "Operator", new { id });
        }
    }
}
