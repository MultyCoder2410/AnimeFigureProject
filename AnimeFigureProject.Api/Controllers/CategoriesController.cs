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
    public class CategoriesController : ControllerBase
    {

        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService
        /// </summary>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public CategoriesController(DataAccessService dataAccessService)
        {
            
            this.dataAccessService = dataAccessService;

        }

        /// <summary>
        /// Gets list of categories from database.
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {

            IEnumerable<Category>? categories = await dataAccessService.GetCategories();

            if (categories == null)
                return NotFound();

            return categories.ToList();

        }

        /// <summary>
        /// Gets specific category from database.
        /// </summary>
        /// <param name="id">Id of specific category</param>
        /// <returns>Specific category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          
            Category? category = await dataAccessService.GetCategory(id);

            if (category == null)
                return NotFound();

            return category;

        }

        /// <summary>
        /// Updated category data in database.
        /// </summary>
        /// <param name="id">Id of category to be updated</param>
        /// <param name="category">Category to be updated</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {

            if (category.Id != id)
                return BadRequest();

            if (await dataAccessService.UpdateCategory(category) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Creates new category in database.
        /// </summary>
        /// <param name="category">New category to be created</param>
        /// <returns>The new category that was created</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {

            if (category == null)
                return BadRequest();

            Category? newCategory = await dataAccessService.CreateCategory(category);

            if (newCategory == null)
                return Problem("newBrand is null");

            return CreatedAtAction("GetCategory", new { id = newCategory.Id }, newCategory);

        }

    }

}
