using AnimeFigureProject.EntityModels;
using AnimeFigureProject.WebApp.Data;
using AnimeFigureProject.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnimeFigureProject.WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> logger;
        private readonly ApiService apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {

            this.logger = logger;
            this.apiService = apiService;

        }

        public async Task<IActionResult> Index()
        {

            AnimeFigure animeFigure = await apiService.GetAnimeFigure(1);

            return View(animeFigure);
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
    }
}