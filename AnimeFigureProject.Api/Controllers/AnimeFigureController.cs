using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnimeFigureProject.EntityModels;
using AnimeFigureProject.DatabaseContext;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
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

            SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            string hashedPassword = Convert.ToBase64String(hashedBytes);

            Collector newCollector = new Collector
            {

                UserName = username,
                Email = email,
                PasswordHash = hashedPassword,
                Collections = new List<Collection>()

            };

            if (userManager == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error: UserManager is null.");

            var result = await userManager.CreateAsync(newCollector, hashedPassword);

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
                return BadRequest("Invalid login attempt.");

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return BadRequest("No user was found.");

            List<Collection> collections = await dbContext?.Collections.Where(c => c.Collectors != null && c.Collectors.Any(c => c.Id == userId)).Include(c => c.AnimeFigures).Include(c => c.Collectors).ToListAsync();

            return Ok(collections);

        }

        // POST api/<AnimeFigureController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AnimeFigureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AnimeFigureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
