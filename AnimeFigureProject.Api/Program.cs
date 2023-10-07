using AnimeFigureProject.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using AnimeFigureProject.EntityModels;

namespace AnimeFigureProject.Api
{

    public class Program
    {

        public static string GenerateRandomSecretKey(int KeyLength)
        {

            byte[] key = new byte[KeyLength];
            using (var rng = RandomNumberGenerator.Create())
            {

                rng.GetBytes(key);

            }

            return Convert.ToBase64String(key);

        }

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddProgramContext("Server=.;Database=AnimeFigureProject;Trusted_Connection=True;TrustServerCertificate=True");
            builder.Services.AddIdentity<Collector, IdentityRole<int>>(options =>
            {

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
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