using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using AnimeFigureProject.WebApp.Models;

namespace AnimeFigureWebApp.Controllers
{

    public class CatalogController : Controller
    {

        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Gets database context so we can communicate with the database.
        /// </summary>
        /// <param name="context">Database context</param>
        public CatalogController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }

        /// <summary>
        /// Gets list of anime figures based on filter and search results.
        /// </summary>
        /// <param name="searchTerm">Name or part of the name of the figure you are searching for</param>
        /// <param name="brands">The brand of the figures you are searching for</param>
        /// <param name="categories">The category of the figures you are searching for</param>
        /// <param name="origins">The origins of the figures you are searching for</param>
        /// <returns>View with an AnimeFigureModel</returns>
        public async Task<IActionResult> Index(string searchTerm, string brands, string categories, string origins, string yearOfReleases)
        {

            int[]? brandIds = null;
            int[]? categoryIds = null;
            int[]? originIds = null;
            int[]? yearOfReleaseIds = null;

            if (!string.IsNullOrEmpty(brands))
            {

                string[] stringBrandIds = brands.Split(",");
                brandIds = Array.ConvertAll(stringBrandIds, int.Parse);

            }

            if (!string.IsNullOrEmpty(categories))
            {

                string[] stringCategoryIds = categories.Split(",");
                categoryIds = Array.ConvertAll(stringCategoryIds, int.Parse);

            }

            if (!string.IsNullOrEmpty(origins))
            {

                string[] stringOriginIds = origins.Split(",");
                originIds = Array.ConvertAll(stringOriginIds, int.Parse);

            }

            if (!string.IsNullOrEmpty(yearOfReleases))
            {

                string[] stringYearOfReleaseIds = yearOfReleases.Split(",");
                yearOfReleaseIds = Array.ConvertAll(stringYearOfReleaseIds, int.Parse);

            }

            List<AnimeFigure>? animeFigures;

            if (string.IsNullOrEmpty(searchTerm) && brandIds == null && categoryIds == null && origins == null)
                animeFigures = await dataAccessService.GetAllAnimeFigures();
            else
                animeFigures = await dataAccessService.GetFilteredAnimeFigures(searchTerm, brandIds, categoryIds, originIds, yearOfReleaseIds);

            AllAnimeFiguresModel model = new AllAnimeFiguresModel
            (

                AnimeFigures: animeFigures,
                Brands: await dataAccessService.GetBrands(),
                Categories: await dataAccessService.GetCategories(),
                Origins: await dataAccessService.GetOrigins()

            );

            return animeFigures != null ? View(model) : Problem("Entity set 'dataAccessService.GetFilteredAnimeFigures'  is null.");

        }

        /// <summary>
        /// This gets the details from the anime figure selected.
        /// </summary>
        /// <param name="id">Id of figure to get from database</param>
        /// <returns>View with the anime figure</returns>
        public async Task<IActionResult> Details(int id)
        {

            var animeFigure = await dataAccessService.GetAnimeFigure(id);

            if (animeFigure == null)
                return NotFound();

            return View(animeFigure);

        }

        /// <summary>
        /// Shows create page for anime figure.
        /// </summary>
        /// <returns>View with NewFigureModel</returns>
        public async Task<IActionResult> Create()
        {

            CreateAnimeFigureModel model = new CreateAnimeFigureModel(

                NewAnimeFigure: new AnimeFigure(),
                Brands: await dataAccessService.GetBrands(),
                Categories: await dataAccessService.GetCategories(),
                Origins: await dataAccessService.GetOrigins()

            );

            return View(model);

        }

        /// <summary>
        /// Handles creation of anime figure.
        /// </summary>
        /// <param name="animeFigure">Contains name, value and price of figure</param>
        /// <param name="brandName">The name of the brand of the figure</param>
        /// <param name="categoryName">The category of the figure</param>
        /// <returns>View of NewFigureModel or redirection to Index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimeFigure animeFigure, string brandName, string categoryName, string selectedOrigins)
        {

            if (ModelState.IsValid)
            {

                string[] allSelectedOrigins = selectedOrigins.Split(",");

                animeFigure.Brand = await dataAccessService.GetBrand(brandName);
                animeFigure.Category = await dataAccessService.GetCategory(categoryName);
                animeFigure.Origins = await dataAccessService.GetOrigins(Array.ConvertAll(allSelectedOrigins, int.Parse));

                await dataAccessService.CreateAnimeFigure(animeFigure);

                return RedirectToAction(nameof(Index));

            }

            CreateAnimeFigureModel model = new CreateAnimeFigureModel(

                NewAnimeFigure: animeFigure,
                Brands: await dataAccessService.GetBrands(),
                Categories: await dataAccessService.GetCategories(),
                Origins: await dataAccessService.GetOrigins()

            );

            return View(model);

        }

        /// <summary>
        /// Checks if anime figure with id exists in database.
        /// </summary>
        /// <param name="id">Id of anime figure to check for</param>
        /// <returns>If anime figure exists</returns>
        private bool AnimeFigureExists(int id) { return dataAccessService.GetAnimeFigure(id) != null; }

    }

}
