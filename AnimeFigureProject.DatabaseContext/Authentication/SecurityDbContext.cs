using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeFigureProject.DatabaseContext.Authentication
{

    public class SecurityDbContext : IdentityDbContext
    {

        public SecurityDbContext() {}
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=.;Database=AnimeFigureProjectSecurity;Trusted_Connection=True;TrustServerCertificate=True");

        }

    }

}
