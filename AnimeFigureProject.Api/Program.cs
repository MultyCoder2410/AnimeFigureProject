using Microsoft.AspNetCore.Identity;
using AnimeFigureProject.DatabaseContext.Data;
using AnimeFigureProject.DatabaseContext.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AnimeFigureProject.DatabaseAccess;

namespace AnimeFigureProject.Api
{

    /// <summary>
    /// Main program class.
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Main function that gets run on startup.
        /// </summary>
        /// <param name="args">Console arguments</param>
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {

                    Version = "v1",
                    Title = "Anime figure collection API",
                    Description = "This is a .net core web API for my anime figure collector application.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {

                        Name = "More info",
                        Url = new Uri("https://github.com/MultyCoder2410/AnimeFigureProject")

                    },
                    License = new OpenApiLicense
                    {

                        Name = "This is the current license that applies.",
                        Url = new Uri("https://github.com/MultyCoder2410/AnimeFigureProject/blob/master/LICENSE.txt")

                    }

                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });

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

            builder.Services.AddScoped<DataAccessService>();

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