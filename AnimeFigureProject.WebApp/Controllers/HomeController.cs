using AnimeFigureProject.DatabaseAccess;
using AnimeFigureProject.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimeFigureProject.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> logger;
        private readonly DataAccessService dataAccessService;

        public HomeController(ILogger<HomeController> logger, DataAccessService dataAccessService)
        {

            this.logger = logger;
            this.dataAccessService = dataAccessService;

        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
        
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }
}