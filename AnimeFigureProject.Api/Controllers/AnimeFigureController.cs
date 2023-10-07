using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigureProject.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnimeFigureController : ControllerBase
    {

        private readonly UserManager<Collector>? userManager;
        private readonly SignInManager<Collector>? signInManager;
        private readonly ApplicationDbContext? dbContext;

        public AnimeFigureController(UserManager<Collector> userManager, SignInManager<Collector> signInManager, ApplicationDbContext dbContext)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password, string username)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
                return BadRequest("Email, password, and username are required.");

            if (userManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: UserManager is null.");

            Collector newCollector = new Collector
            {

                UserName = email,
                Email = email,
                Collections = new List<Collection>()

            };

            var result = await userManager.CreateAsync(newCollector, password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Registration successful");

        }

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

        // GET: api/<AnimeFigureController>
        [HttpGet("animefigures")]
        public IActionResult GetAnimeFigures()
        {

            return Ok(dbContext.AnimeFigures);

        }

        // GET api/<AnimeFigureController>/5
        [HttpGet("animefigure/{id}")]
        public async Task<IActionResult> GetAnimeFigure(int id)
        {

            AnimeFigure? animeFigure = await dbContext?.AnimeFigures?.SingleOrDefaultAsync(f => f.Id == id);

            if (animeFigure == null)
                return BadRequest("Invalid anime figure id.");

            return Ok(animeFigure);

        }

        [Authorize]
        [HttpGet("collections")]
        public async Task<IActionResult> GetCollections()
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            List<Collection> collections = await dbContext?.Collections.Where(c => c.Collectors != null && c.Collectors.Any(c => c.Id == int.Parse(userId))).Include(c => c.AnimeFigures).Include(c => c.Collectors).ToListAsync();

            return Ok(collections);

        }

        [Authorize]
        [HttpGet("collection")]
        public async Task<IActionResult> GetCollection(int id)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            Collection collection = await dbContext?.Collections.FirstOrDefaultAsync(c => c.Id == id && c.Collectors.Any(c => c.Id == int.Parse(userId)));

            if (collection == null)
                return NotFound();

            return Ok(collection);

        }

        [Authorize]
        [HttpPost("collection")]
        public async Task<IActionResult> PostCollection(string name)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            Collection collection = new Collection
            {

                Name = name,
                TotalPrice = 0,
                TotalValue = 0,
                OwnerId = int.Parse(userId),
                AnimeFigures = new List<AnimeFigure>()

            };

            dbContext?.Collections?.AddAsync(collection);
            await dbContext?.SaveChangesAsync();

            return Ok();

        }

        [HttpPut("collection/{id}")]
        public async Task<IActionResult> PutCollection(int id, string name)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            Collection? collection = await dbContext?.Collections?.FirstOrDefaultAsync(c => c.Id == id && c.OwnerId == int.Parse(userId));

            if (collection == null)
                return NotFound();

            collection.Name = name;
            dbContext.SaveChanges();

            return Ok();

        }

        [HttpDelete("collection/{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            Collection? collection = await dbContext?.Collections?.FirstOrDefaultAsync(c => c.Id == id && c.OwnerId == int.Parse(userId));

            if (collection == null)
                return NotFound();

            dbContext?.Collections?.Remove(collection);
            await dbContext?.SaveChangesAsync();

            return Ok();

        }

    }
}
