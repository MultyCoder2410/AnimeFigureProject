using Microsoft.AspNetCore.Identity;
using AnimeFigureProject.DatabaseContext.Data;
using AnimeFigureProject.DatabaseContext.Authentication;

namespace AnimeFigureProject.Api
{

    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

           

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddProgramContext("Server=.;Database=AnimeFigureProjectData;Trusted_Connection=True;TrustServerCertificate=True");
            builder.Services.AddSecurityContext("Server=.;Database=AnimeFigureProjectSecurity;Trusted_Connection=True;TrustServerCertificate=True");

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;

            })
            .AddEntityFrameworkStores<SecurityDbContext>()
            .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }

    }

}