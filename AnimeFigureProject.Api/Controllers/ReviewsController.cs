using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// This controller allows you to modify and view reviews withing the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService
        /// </summary>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public ReviewsController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }

        //// <summary>
        /// Gets list of reviews from database.
        /// </summary>
        /// <returns>List of reviews</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {

            IEnumerable<Review>? reviews = await dataAccessService.GetReviews();

            if (reviews == null)
                return NotFound();

            return reviews.ToList();

        }

        /// <summary>
        /// Gets specific category from database.
        /// </summary>
        /// <param name="id">Id of specific category</param>
        /// <returns>Specific category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {

            Review? review = await dataAccessService.GetReview(id);

            if (review == null)
                return NotFound();

            return review;

        }

        /// <summary>
        /// Updated review data in database.
        /// </summary>
        /// <param name="id">Id of review to be updated</param>
        /// <param name="review">Review to be updated</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {

            if (review.Id != id)
                return BadRequest();

            if (await dataAccessService.UpdateReview(review) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Creates new review in database.
        /// </summary>
        /// <param name="review">New review to be created</param>
        /// <returns>The new review that was created</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {

            if (review == null)
                return BadRequest();

            Review? newReview = await dataAccessService.CreateReview(review);

            if (newReview == null)
                return Problem("newOrigin is null");

            return CreatedAtAction("GetOrigin", new { id = newReview.Id }, newReview);

        }

        /// <summary>
        /// Deletes review from database.
        /// </summary>
        /// <param name="id">Id of review to be deleted</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {

            string? loggedinUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedinUser == null)
                return BadRequest("No user found.");

            if (await dataAccessService.DeleteReview(id, loggedinUser) == false)
                return NotFound();

            return NoContent();

        }

    }

}
