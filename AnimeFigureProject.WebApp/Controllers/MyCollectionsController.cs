using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AnimeFigureProject.WebApp.Controllers
{

    [Authorize]
    public class MyCollectionsController : Controller
    {

        private readonly DataAccessService dataAccessService;

        public MyCollectionsController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }

        // GET: MyCollections
        public async Task<IActionResult> Index()
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("Must be logged in to use function.");

            List<Collection>? collections = await dataAccessService.GetCollections(loggedinUser);

            return View(collections);

        }

        // POST: MyCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("Must be logged in to use function.");

            Collector? Owner = await dataAccessService.GetCollector(loggedinUser);

            if (Owner == null)
                return BadRequest("Owner not found.");

            Collection collection = new Collection
            {

                OwnerId = Owner.Id,
                Name = name,
                TotalPrice = 0,
                TotalValue = 0

            };

            await dataAccessService.CreateCollection(collection);

            return RedirectToAction(nameof(Index));

        }

        // GET: MyCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("Must be logged in to use function.");

            Collection? collection = await dataAccessService.GetCollection(id, loggedinUser);

            return View(collection);

        }

        // POST: MyCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TotalPrice,TotalValue,OwnerId")] Collection collection)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("Must be logged in to use function.");

            collection = await dataAccessService.GetCollection(id, loggedinUser);

            return View(collection);

        }

        // GET: MyCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("Must be logged in to use function.");

            Collection? collection = await dataAccessService.GetCollection(id, loggedinUser);

            return View(collection);

        }

        // POST: MyCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            return RedirectToAction(nameof(Index));

        }

        private bool CollectionExists(int id) 
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return false;

            return dataAccessService.GetCollection(id, loggedinUser) != null; 
        
        }

    }

}