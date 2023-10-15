using AnimeFigureProject.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigureProject.DatabaseContext.Data
{

    public class ApplicationDbContext : DbContext
    {

        public DbSet<AnimeFigure>? AnimeFigures { get; set; }
        public DbSet<Brand>? Brands { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Collection>? Collections { get; set; }
        public DbSet<Collector>? Collectors { get; set; }
        public DbSet<Origin>? Origins { get; set; }
        public DbSet<Review>? Reviews { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=.;Database=AnimeFigureProjectData;Trusted_Connection=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }

    }

}
