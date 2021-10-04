using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ZgloszeniaIT.Services;
namespace ZgloszeniaIT.Views
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly IApplicationRepository _applicationRepository;

        public AdminController(IApplicationRepository applicationRepository, ILogger<AdminController> logger)
        {
            _logger = logger;
            _applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
