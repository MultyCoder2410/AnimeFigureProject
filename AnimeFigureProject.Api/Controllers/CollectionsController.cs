using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeFigureProject.EntityModels;
using Microsoft.AspNetCore.Authorization;
using AnimeFigureProject.DatabaseAccess;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// This controller allows you to modify and view collectios withing the database.
    /// You need to be logged in with collector to view collection.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {

        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService
        /// </summary>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public CollectionsController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }


        /// <summary>
        /// Gets list of collections from database.
        /// </summary>
        /// <returns>List of collections</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            IEnumerable<Collection>? collections = await dataAccessService.GetCollections(loggedinUser);

            if (collections == null)
                return NotFound();

            return collections.ToList();

        }

        /// <summary>
        /// Gets specific collection from database.
        /// </summary>
        /// <param name="id">Id of specific collection</param>
        /// <returns>Specific collection</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            Collection? collection = await dataAccessService.GetCollection(id, loggedinUser);

            if (collection == null)
                return NotFound();

            return collection;

        }

        /// <summary>
        /// Updated collection data in database.
        /// </summary>
        /// <param name="id">Id of collection to be updated</param>
        /// <param name="collection">Collection to be updated</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {

            if (id != collection.Id)
                return BadRequest();

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            if (await dataAccessService.UpdateCollection(collection) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Adds anime figure to collection
        /// </summary>
        /// <param name="id">Id of collection to add anime figure to</param>
        /// <param name="animeFigureId">Id of anime figure to add</param>
        /// <param name="animeFigure">Anime figure to add</param>
        /// <returns>No content</returns>
        [HttpPost("{id}/{animeFigureId}")]
        public async Task<IActionResult> AddAnimeFigureToCollection(int id, int animeFigureId, AnimeFigure animeFigure)
        {

            if (animeFigure.Id != animeFigureId)
                return BadRequest();

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            if (await dataAccessService.AddAnimeFigureToCollection(id, animeFigure, loggedinUser) == null)
                return NotFound();

            return Ok();

        }

        /// <summary>
        /// Removes anime figure from collection.
        /// </summary>
        /// <param name="id">Id of collection to remove the anime figure from</param>
        /// <param name="animeFigureId">Id of anime figure to remove</param>
        /// <param name="animeFigure">The anime figure to remove</param>
        /// <returns></returns>
        [HttpDelete("{id}/{animeFigureId}")]
        public async Task<IActionResult> RemoveAnimeFigureFromCollection(int id, int animeFigureId, AnimeFigure animeFigure)
        {

            if (animeFigureId != animeFigure.Id)
                return BadRequest();

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            if (await dataAccessService.RemoveAnimeFigureFromCollection(id, animeFigure, loggedinUser) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Shares collection with other user.
        /// </summary>
        /// <param name="id">Id of collection to be shared</param>
        /// <param name="collectorId">Id of the collector to share the collection with</param>
        /// <param name="collector">The collector to share the collection with</param>
        /// <returns>Collection shared</returns>
        [HttpPut("{id}/{collectorId}")]
        public async Task<IActionResult> ShareCollection(int id, int collectorId, Collector collector)
        {

            if (collector.Id != collectorId)
                return BadRequest();

            if (await dataAccessService.ShareCollection(id, collector) == null)
                return NotFound();

            return Ok();

        }

        /// <summary>
        /// Creates new collection in database.
        /// </summary>
        /// <param name="collection">New collection to be created</param>
        /// <returns>The new collection that was created</returns>
        [HttpPost]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {

            if (collection == null)
                return BadRequest();

            Collection? newCollection = await dataAccessService.CreateCollection(collection);

            if (newCollection == null)
                return Problem("newCollection is null");

            return CreatedAtAction("GetCollection", new { id = newCollection.Id }, newCollection);

        }

        /// <summary>
        /// Deletes collection from database.
        /// </summary>
        /// <param name="id">Id of collection to be deleted</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            if (await dataAccessService.DeleteCollection(id, loggedinUser) == false)
                return NotFound();

            return NoContent();

        }

    }

}
