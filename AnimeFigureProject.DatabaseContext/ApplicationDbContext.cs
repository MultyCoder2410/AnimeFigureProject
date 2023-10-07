using AnimeFigureProject.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigureProject.DatabaseContext
{
    
    public class ApplicationDbContext : IdentityDbContext<Collector, IdentityRole<int>, int>
    {

        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
        public DbSet<AnimeFigure>? AnimeFigures { get; set; }
        public DbSet<Brand>? Brands {  get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Collection>? Collections { get; set; }
        public DbSet<Collector>? Collectors { get; set; }
        public DbSet<Origin>? Origins { get; set; }
        public DbSet<Review>? Reviews {  get; set; }
        public DbSet<EntityModels.Type>? Types {  get; set; }

        public ApplicationDbContext() {}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=.;Database=AnimeFigureProject;Trusted_Connection=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }

    }

}
