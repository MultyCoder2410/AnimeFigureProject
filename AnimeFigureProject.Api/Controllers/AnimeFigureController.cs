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

        [HttpGet("filteredanimefigures")]
        public IActionResult GetFilteredAnimeFigures(string? searchTerm, [FromQuery] int[]? brandIds, [FromQuery] int[]? TypeIds, [FromQuery] int[]? originIds)
        {

            IQueryable<AnimeFigure> animeFigures = dbContext?.AnimeFigures;

            if (animeFigures == null)
                return NotFound();

            if (!string.IsNullOrEmpty(searchTerm))
                animeFigures = animeFigures.Where(f => f.Name != null && f.Name.Contains(searchTerm));

            if (brandIds != null && brandIds.Length > 0)
                animeFigures = animeFigures.Where(f => f.Brand != null && brandIds.Contains(f.Brand.Id));

            if (TypeIds != null && TypeIds.Length > 0)
                animeFigures = animeFigures.Where(f => f.Type != null && TypeIds.Contains(f.Type.Id));

            if (originIds != null && originIds.Length > 0)
            {

                var filteredFigures = animeFigures.Include(f => f.Origins).AsEnumerable().Where(f => f.Origins != null && f.Origins.Any(origin => originIds.Contains(origin.Id))).Select(f => f.Id);
                animeFigures = animeFigures.Where(f => filteredFigures.Contains(f.Id));

            }

            return Ok(animeFigures);

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

            return Ok(dbContext?.Types);

        }

        [HttpGet("type/{id}")]
        public async Task<IActionResult> GetType(int id)
        {

            EntityModels.Type? type = await dbContext?.Types?.FirstOrDefaultAsync(t => t.Id == id);

            if (type == null)
                return NotFound();

            return Ok(type);

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

            return Ok(dbContext?.Brands);

        }

        [HttpGet("brand/{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {

            Brand? brand = await dbContext?.Brands?.FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null)
                return NotFound();

            return Ok(brand);

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

            return Ok(dbContext?.Origins);

        }

        [HttpGet("origin/{id}")]
        public async Task<IActionResult> GetOrigin(int id)
        {

            Origin origin = await dbContext?.Origins?.FirstOrDefaultAsync(o => o.Id == id);

            if (origin == null)
                return NotFound();

            return Ok(origin);

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

            return Ok(dbContext?.Categories);

        }

        [HttpGet("categorie/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            Category category = await dbContext?.Categories?.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);

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

            return Ok(dbContext?.Reviews);

        }

        [HttpGet("review/{id}")]
        public async Task<IActionResult> GetReview(int id)
        {

            Review review = await dbContext?.Reviews?.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                return NotFound();

            return Ok(review);

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

        [Authorize]
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

        [Authorize]
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
