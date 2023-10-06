using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeFigureProject.DatabaseContext
{
    
    public static class ApplicationContextExtension
    {

        public static IServiceCollection AddProgramContext(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseSqlServer(connectionString);
                options.LogTo(Console.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });

            });

            return services;

        }

    }

}
