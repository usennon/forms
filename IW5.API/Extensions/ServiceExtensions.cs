using IW5.BL.API;
using IW5.BL.API.Contracts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.DAL;
using Microsoft.EntityFrameworkCore;

namespace IW5.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureLogic(this IServiceCollection services)
        {
            services.AddScoped<IFormBLogic, FormLogic>();
            services.AddScoped<IQuestionBLogic, QuestionLogic>();
            services.AddScoped<IOptionBLogic, OptionLogic>();
            services.AddScoped<IUserBLogic, UserLogic>();

        }
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                options.AddPolicy("AllowBlazorApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:5001")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
            });

        public static void ConfigureSqlContext(this IServiceCollection services, string? connectionString) =>
            services.AddDbContextPool<FormsDbContext>(options => options.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
    }
}
