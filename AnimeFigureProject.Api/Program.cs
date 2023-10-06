using AnimeFigureProject.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "https://localhost:7217",
                    ValidAudience = "https://localhost:7217",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerateRandomSecretKey(256)))
                };

            });

            builder.Services.AddProgramContext("Server=.;Database=AnimeFigureProject;Trusted_Connection=True;TrustServerCertificate=True");
            builder.Services.AddIdentity<Collector, IdentityRole>(options =>
            {

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

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