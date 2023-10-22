using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeFigureProject.DatabaseContext.Data;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using Microsoft.AspNetCore.Authorization;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// This controller allows you to modify and view anime figures withing the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeFiguresController : ControllerBase
    {
        
        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService
        /// </summary>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public AnimeFiguresController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }

        /// <summary>
        /// Gets list of anime figures from database.
        /// </summary>
        /// <returns>List of anime figures</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimeFigure>>> GetAnimeFigures()
        {

            IEnumerable<AnimeFigure>? animeFigures = await dataAccessService.GetAllAnimeFigures();

            if (animeFigures == null)
                return NotFound();

            return animeFigures.ToList();

        }

        /// <summary>
        /// Gets filterd list of anime figures from database.
        /// </summary>
        /// <returns>Filtered list of anime figures</returns>
        [HttpGet("FilteredAnimeFigures")]
        public async Task<ActionResult<IEnumerable<AnimeFigure>>> GetFilteredAnimeFigures([FromQuery] string? searchTerm, [FromQuery] int[]? brandIds, [FromQuery] int[]? categoryIds, [FromQuery] int[]? originIds)
        {

            IEnumerable<AnimeFigure>? animeFigures = await dataAccessService.GetFilteredAnimeFigures(searchTerm, brandIds, categoryIds, originIds, null);

            if (animeFigures == null)
                return NotFound();

            return animeFigures.ToList();

        }

        /// <summary>
        /// Gets specific anime figure from database.
        /// </summary>
        /// <param name="id">Id of specific anime figure</param>
        /// <returns>Specific anime figure</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeFigure>> GetAnimeFigure(int id)
        {

            AnimeFigure? animeFigure = await dataAccessService.GetAnimeFigure(id);

            if (animeFigure == null)
                return NotFound();

            return animeFigure;

        }

        /// <summary>
        /// Updated anime figure data in database.
        /// </summary>
        /// <param name="id">Id of anime figure to be updated</param>
        /// <param name="animeFigure">Anime figure to be updated</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimeFigure(int id, AnimeFigure animeFigure)
        {

            if (animeFigure.Id != id)
                return BadRequest();

            if (await dataAccessService.UpdateAnimeFigure(animeFigure) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Creates new anime figure in database.
        /// </summary>
        /// <param name="animeFigure">New anime figure to be created</param>
        /// <returns>The new anime figure that was created</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AnimeFigure>> PostAnimeFigure(AnimeFigure animeFigure)
        {

            if (animeFigure == null)
                return BadRequest();

            AnimeFigure? newAnimeFigure = await dataAccessService.CreateAnimeFigure(animeFigure);

            if (newAnimeFigure == null)
                return Problem("newAnimeFigure is null");

            return CreatedAtAction("GetAnimeFigure", new { id = newAnimeFigure.Id }, newAnimeFigure);

        }

    }

}
