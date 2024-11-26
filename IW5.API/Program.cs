using IW5.DAL.Initialization;
using IW5.DAL;
using IW5.API.Extensions;
using IW5.API.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IW5.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Read configuration from appsettings.json
            var configuration = builder.Configuration;

            builder.Services.ConfigureCors();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();


            var connectionString = configuration.GetConnectionString("IW5");

            builder.Services.ConfigureSqlContext(connectionString);
            builder.Services.AddAutoMapper(typeof(Program));

            
            var app = builder.Build();
            // At the time of the evaluation of phase 1, the check for in development or production was removed.
            // Since the website is in production, and we would like to give teachers the opportunity to evaluate it remotely without launching it locally.
            
                if (configuration.GetValue<bool>("RebuildDataBase"))
                {
                    SampleDataInitializer.InitializeData(new FormsDbContext(
                                               new DbContextOptionsBuilder<FormsDbContext>()
                                                                          .UseSqlServer(connectionString).Options));
                }

                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        void ConfigureAuthentication(IServiceCollection serviceCollection, string identityServerUrl)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = identityServerUrl;
                    options.TokenValidationParameters.ValidateAudience = false;
                });

            serviceCollection.AddAuthorization(
                options =>
                {
                    options.AddPolicy(ApiPolicies.IngredientAdmin, policy => policy.RequireRole(AppRoles.Admin));
                });
            serviceCollection.AddHttpContextAccessor();
        }
    }
}
