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

        /// <summary>
        /// Returns the view of the home page with all the animefigures.
        /// </summary>
        /// <returns>View of home page with animefigures</returns>
        public async Task<IActionResult> Index()
        {

            return View(await dataAccessService.GetAllAnimeFigures());

        }

        /// <summary>
        /// Returns error view with some error information.
        /// </summary>
        /// <returns>Error view</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
        
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }

}