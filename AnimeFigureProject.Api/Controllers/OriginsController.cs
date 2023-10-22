using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using Microsoft.AspNetCore.Authorization;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// This controller allows you to modify and view categories withing the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OriginsController : ControllerBase
    {

        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService
        /// </summary>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public OriginsController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }

        /// <summary>
        /// Gets list of origins from database.
        /// </summary>
        /// <returns>List of origins</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Origin>>> GetOrigins()
        {

            IEnumerable<Origin>? origins = await dataAccessService.GetOrigins();

            if (origins == null)
                return NotFound();

            return origins.ToList();

        }

        /// <summary>
        /// Gets specific category from database.
        /// </summary>
        /// <param name="id">Id of specific category</param>
        /// <returns>Specific category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Origin>> GetOrigin(int id)
        {

            Origin? origin = await dataAccessService.GetOrigin(id);

            if (origin == null)
                return NotFound();

            return origin;

        }

        /// <summary>
        /// Updated origin data in database.
        /// </summary>
        /// <param name="id">Id of origin to be updated</param>
        /// <param name="origin">Origin to be updated</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrigin(int id, Origin origin)
        {

            if (origin.Id != id)
                return BadRequest();

            if (await dataAccessService.UpdateOrigin(origin) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Creates new origin in database.
        /// </summary>
        /// <param name="origin">New origin to be created</param>
        /// <returns>The new origin that was created</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Origin>> PostOrigin(Origin origin)
        {

            if (origin == null)
                return BadRequest();

            Origin? newOrigin = await dataAccessService.CreateOrigin(origin);

            if (newOrigin == null)
                return Problem("newOrigin is null");

            return CreatedAtAction("GetOrigin", new { id = newOrigin.Id }, newOrigin);

        }

    }

}
