
using BusinessLayer;
using DataAccessLayer;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IDemandesService, DemandesService>();
            builder.Services.AddScoped<IDemandesRepository, DemandesRepository>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("ConnectDb");
                return new DemandesRepository(new SqlConnection(connectionString));
            });

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(auth =>
                {
                    auth.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
                    auth.Audience = builder.Configuration["Auth0:Audience"];
                    auth.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("employee", policy => policy.RequireClaim("permissions", "employee"));
                options.AddPolicy("administrator", policy => policy.RequireClaim("permissions", "administrator"));
                options.AddPolicy("Manager", policy => policy.RequireClaim("permissions", "Manager"));
            });

            var app = builder.Build();
            app.UseCors(myAllowSpecificOrigins);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
