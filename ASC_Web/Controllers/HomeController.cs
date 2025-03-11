using System.Diagnostics;
using ASC.Utilities;
using ASC_Web.Configuration;
using ASC_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASC_Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;

        public IOptions<ApplicationSetting> _settings;

        public HomeController(ILogger<HomeController> logger, IOptions<ApplicationSetting> settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public ActionResult Index()
        {
            // Set Session
            HttpContext.Session.SetSession("Test", _settings.Value);
            // Get Session
            var settings = HttpContext.Session.GetSession<ApplicationSetting>("Test");
            // Usage of IOptions
            ViewBag.Title = _settings.Value.ApplicationTitle;

            //Test fail test case
            //ViewData.Model = "Test";
            //throw new Exception("Login Fail!!!");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
