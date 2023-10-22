using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using Microsoft.AspNetCore.Authorization;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// This controller allows you to modify and view brands withing the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService
        /// </summary>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public BrandsController(DataAccessService dataAccessService)
        {

            this.dataAccessService = dataAccessService;

        }

        /// <summary>
        /// Gets list of all brands.
        /// </summary>
        /// <returns>List of all brands</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            
            IEnumerable<Brand>? brands = await dataAccessService.GetBrands();

            if (brands == null)
                return NotFound();

            return brands.ToList();

        }

        /// <summary>
        /// Gets brand with specific id.
        /// </summary>
        /// <param name="id">Id of specific brand</param>
        /// <returns>Brand of the specific id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
          
            Brand? brand = await dataAccessService.GetBrand(id);

            if (brand == null) 
                return NotFound();

            return brand;

        }

        /// <summary>
        /// Updates brand data with database.
        /// </summary>
        /// <param name="id">Id of brand to be edited</param>
        /// <param name="brand">Brand to be edited</param>
        /// <returns>No content</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {

            if (brand.Id != id)
                return BadRequest();

            if (await dataAccessService.UpdateBrand(brand) == null)
                return NotFound();

            return NoContent();

        }

        /// <summary>
        /// Creates new brand in database.
        /// </summary>
        /// <param name="brand">New brand to be created</param>
        /// <returns>The new brand that was created</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            
            if (brand == null)
                return BadRequest();

            Brand? newBrand = await dataAccessService.CreateBrand(brand);

            if (newBrand == null)
                return Problem("newBrand is null");

            return CreatedAtAction("GetBrand", new { id = newBrand.Id }, newBrand);

        }

    }

}
