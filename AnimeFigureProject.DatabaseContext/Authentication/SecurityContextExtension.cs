using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeFigureProject.DatabaseContext.Authentication
{

    public static class SecurityContextExtension
    {

        /// <summary>
        /// Makes it easy and quick to add the database to the project
        /// </summary>
        /// <param name="services">Service you are using</param>
        /// <param name="connectionString">The connection string for the security databse</param>
        /// <returns>The services given</returns>
        public static IServiceCollection AddSecurityContext(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<SecurityDbContext>(options =>
            {

                options.UseSqlServer(connectionString);
                options.LogTo(Console.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });

            });

            return services;

        }

    }

}
