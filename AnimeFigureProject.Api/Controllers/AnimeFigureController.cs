using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnimeFigureProject.DatabaseAccess;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// Api for accessing anime figure data and user specific data.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeFigureController : ControllerBase
    {

        private readonly UserManager<IdentityUser>? userManager;
        private readonly SignInManager<IdentityUser>? signInManager;
        private readonly DataAccessService? dataAccessService;

        /// <summary>
        /// Creates anime figure controller
        /// </summary>
        /// <param name="userManager">Used for creating and deleting users</param>
        /// <param name="signInManager">Used for loggin in and out</param>
        /// <param name="dataAccessService">Used for accessing the database</param>
        public AnimeFigureController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DataAccessService dataAccessService)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dataAccessService = dataAccessService;

        }
        /*
        /// <summary>
        /// Registers a new user to the database.
        /// </summary>
        /// <param name="email">The mail of the new user</param>
        /// <param name="password">The password of the new user</param>
        /// <param name="username">The username of the new user</param>
        /// <returns>If was able to create user</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password, string username)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
                return BadRequest("Email, password, and username are required.");

            if (userManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: UserManager is null.");

            IdentityUser newUser = new IdentityUser
            {

                UserName = email,
                Email = email,

            };

            var result = await userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            Collector newCollector = new Collector
            {

                Name = username,
                Collections = new List<Collection>(),
                AuthenticationUserId = newUser.Id

            };

            dataAccessService?.CreateCollector(newCollector);

            return Ok("Registration successful");

        }

        /// <summary>
        /// Login to API with user.
        /// </summary>
        /// <param name="email">Mail address of user</param>
        /// <param name="password">Password of user</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return BadRequest("Email and password are required.");

            if (signInManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: signInManager is null.");

            var result = await signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
                return BadRequest("Invalid login attempt. Details: " + string.Join(", ", result.ToString()));

            return Ok("Login successful");

        }

        /// <summary>
        /// Logs out user from api.
        /// </summary>
        /// <returns>Was logout succesful</returns>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            if (signInManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: signInManager is null.");

            await signInManager.SignOutAsync();

            return Ok("Logout succesful");

        }

        /// <summary>
        /// Deletes user account
        /// </summary>
        /// <returns>Was able to delete user</returns>
        [Authorize]
        [HttpDelete("account")]
        public async Task<IActionResult> DeleteAccount()
        {

            if (userManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: UserManager is null.");

            if (signInManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: signInManager is null.");

            IdentityUser? user = await userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("No user was found to delete.");

            string userId = user.Id;

            await signInManager.SignOutAsync();
            IdentityResult result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest("\"Failed to delete account. Details: " + result.Errors.ToString());

            dataAccessService?.DeleteCollector(dataAccessService.GetCollector(userId).Id);

            return Ok("Deleted account.");

        }

        /// <summary>
        /// Gets all anime figures stored in database
        /// </summary>
        /// <returns>List of all anime figures in database</returns>
        [HttpGet("animefigures")]
        public IActionResult GetAnimeFigures()
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(dataAccessService?.GetAllAnimeFigures());

        }

        /// <summary>
        /// Gets all anime figures stored in database based on filters.
        /// </summary>
        /// <param name="searchTerm">Anime figure must contain in name</param>
        /// <param name="brandIds">The brands that you want to see the anime figure from.</param>
        /// <param name="TypeIds">The types of anime figures you want to see</param>
        /// <param name="originIds">The origins of the anime figures you want to see</param>
        /// <returns>List of filtered anime figures</returns>
        [HttpGet("filteredanimefigures")]
        public IActionResult GetFilteredAnimeFigures(string? searchTerm, [FromQuery] int[]? brandIds, [FromQuery] int[]? TypeIds, [FromQuery] int[]? originIds)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(dataAccessService.GetFilteredAnimeFigures(searchTerm, brandIds, TypeIds, originIds));

        }


        /// <summary>
        /// Gets specific anime figure.
        /// </summary>
        /// <param name="id">Id of anime figure</param>
        /// <returns>Anime figure which has the given id</returns>
        [HttpGet("animefigure/{id}")]
        public async Task<IActionResult> GetAnimeFigure(int id)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetAnimeFigure(id));

        }

        [Authorize]
        [HttpPost("animefigure")]
        public async Task<IActionResult> PostAnimeFigure()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("animefigure/{id}")]
        public async Task<IActionResult> PutAnimeFigure()
        {

            return Ok();

        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetTypes());

        }

        [HttpGet("type/{id}")]
        public async Task<IActionResult> GetType(int id)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetType(id));

        }

        [Authorize]
        [HttpPost("type")]
        public async Task<IActionResult> PostType()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("type/{id}")]
        public async Task<IActionResult> PutType(int id)
        {

            return Ok();

        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {

            return Ok(await dataAccessService?.GetBrands());

        }

        [HttpGet("brand/{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetBrand(id));

        }

        [Authorize]
        [HttpPost("brand")]
        public async Task<IActionResult> PostBrand()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("brand/{id}")]
        public async Task<IActionResult> PutBrand(int id)
        {

            return Ok();

        }

        [HttpGet("origins")]
        public async Task<IActionResult> GetOrigins()
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetOrigins());

        }

        [HttpGet("origin/{id}")]
        public async Task<IActionResult> GetOrigin(int id)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetOrigin(id));

        }
        
        [Authorize]
        [HttpPost("origin")]
        public async Task<IActionResult> PostOrigin()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("origin/{id}")]
        public async Task<IActionResult> PutOrigin(int id)
        {

            return Ok();

        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetCategories());

        }

        [HttpGet("categorie/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetCategory(id));

        }

        [Authorize]
        [HttpPost("category")]
        public async Task<IActionResult> PostCategory()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("category/{id}")]
        public async Task<IActionResult> PutCategory(int id)
        {

            return Ok();

        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetReviews()
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetReviews());

        }

        [HttpGet("review/{id}")]
        public async Task<IActionResult> GetReview(int id)
        {

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetReview(id));

        }

        [Authorize]
        [HttpPost("review")]
        public async Task<IActionResult> PostReview()
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("review/{id}")]
        public async Task<IActionResult> PutReview(int id)
        {

            return Ok();

        }

        [Authorize]
        [HttpDelete("review/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {

            return Ok();

        }

        /// <summary>
        /// Gets all collections of user.
        /// </summary>
        /// <returns>All collections from user</returns>
        [Authorize]
        [HttpGet("collections")]
        public async Task<IActionResult> GetCollections()
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetCollections(userId));

        }

        /// <summary>
        /// Gets specific collection from user.
        /// </summary>
        /// <param name="id">Id of collection</param>
        /// <returns>Specifik collection from user</returns>
        [Authorize]
        [HttpGet("collection")]
        public async Task<IActionResult> GetCollection(int id)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            return Ok(await dataAccessService.GetCollection(id, userId));

        }

        [Authorize]
        [HttpPost("collection")]
        public async Task<IActionResult> PostCollection(string name)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            Collection collection = new Collection
            {

                Name = name,
                TotalPrice = 0,
                TotalValue = 0,
                OwnerId = dataAccessService.GetCollector(userId).Id,
                AnimeFigures = new List<AnimeFigure>()

            };

            return Ok(await dataAccessService.CreateCollection(collection));

        }

        /// <summary>
        /// Updates collection name of collection from current user.
        /// </summary>
        /// <param name="id">Id of the choosen collection</param>
        /// <param name="name">New name of collection</param>
        /// <returns>Update of collection was succesfull</returns>
        [Authorize]
        [HttpPut("collection/{id}")]
        public async Task<IActionResult> PutCollection(int id, string name)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            Collection? collection = await dataAccessService.GetCollection(id, userId);

            if (collection == null)
                return NotFound("Could not find collection");

            collection.Name = name;

            return Ok(dataAccessService.UpdateCollection(collection));

        }

        [Authorize]
        [HttpPut("collection/addfigure/{id}")]
        public async Task<IActionResult> PutCollectionFigure(int id)
        {

            return Ok();

        }

        [Authorize]
        [HttpPut("collection/removefigure/{id}")]
        public async Task<IActionResult> PutCollectionRemoveFigure(int id)
        {

            return Ok();

        }

        /// <summary>
        /// Deletes collection from logged in user.
        /// </summary>
        /// <param name="id">The collection id which needs to be deleted</param>
        /// <returns>Was deletion succesfull</returns>
        [Authorize]
        [HttpDelete("collection/{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            if (dataAccessService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: dataAccessService is null.");

            await dataAccessService.DeleteCollection(id, userId);

            return Ok("Deleted collection");

        }
        */
    }
}
