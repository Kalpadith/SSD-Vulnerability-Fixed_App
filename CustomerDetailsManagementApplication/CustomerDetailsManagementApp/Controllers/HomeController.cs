using CustomerDetailsManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DatabaseConfigClassLibrary.DataManipulate;

namespace CustomerDetailsManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataImporter _dataImporter;

        public HomeController(ILogger<HomeController> logger, DataImporter dataImporter)
        {
            _logger = logger;
            _dataImporter = dataImporter;
        }

        public IActionResult Index()
        {
            _dataImporter.ImportDataFromJson();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
