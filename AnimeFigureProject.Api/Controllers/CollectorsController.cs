using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AnimeFigureProject.Api.Controllers
{

    /// <summary>
    /// This controller allows you to modify and view collectors withing the database. 
    /// It also allows you to login and out of the api with your personal account.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CollectorsController : ControllerBase
    {

        private readonly UserManager<IdentityUser>? userManager;
        private readonly SignInManager<IdentityUser>? signInManager;
        private readonly DataAccessService dataAccessService;

        /// <summary>
        /// Sets dataAccessService, user manager and signin manager
        /// </summary>
        /// <param name="userManager">Used for creating and deleting users</param>
        /// <param name="signInManager">Used for loggin in and out</param>
        /// <param name="dataAccessService">Current DataAccessService used by program</param>
        public CollectorsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DataAccessService dataAccessService)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dataAccessService = dataAccessService;

        }

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

                UserName = username,
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

            await dataAccessService.CreateCollector(newCollector);

            return Ok("Registration successful");

        }

        /// <summary>
        /// Login to API with user.
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest("Email and password are required.");

            if (signInManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: signInManager is null.");

            var result = await signInManager.PasswordSignInAsync(username, password, false, false);

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

            await dataAccessService.DeleteCollector(dataAccessService.GetCollector(userId).Id);

            return Ok("Deleted account.");
        }

        /// <summary>
        /// Returns list of collectors without collections and authentication id.
        /// </summary>
        /// <returns>List of collectors</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collector>>> GetCollectors()
        {

            IEnumerable<Collector>? collectors = await dataAccessService.GetCollectors();

            if (collectors == null)
                return NotFound();

            return collectors.ToList();

        }

        /// <summary>
        /// Gets current logged in collector from database.
        /// </summary>
        /// <returns>Logged in collector</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Collector>> GetLoggedInCollector(int id)
        {

            if (userManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: UserManager is null.");

            IdentityUser? user = await userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("No user was found.");

            Collector? collector = await dataAccessService.GetCollector(user.Id);

            if (collector == null || collector.Id != id)
                return NotFound("No collector was found");

            return collector;

        }

        /// <summary>
        /// Updates current logged in collector.
        /// </summary>
        /// <param name="id">Id of current collector</param>
        /// <param name="collector">Current collector</param>
        /// <returns>Updated collector</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoggedInCollector(int id, Collector collector)
        {

            if (userManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: UserManager is null.");

            if (collector.Id != id)
                return BadRequest();

            IdentityUser? user = await userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("No user was found.");

            if (collector.AuthenticationUserId != user.Id)
                return BadRequest();

            await dataAccessService.UpdateCollector(collector);

            return NoContent();

        }

    }

}
