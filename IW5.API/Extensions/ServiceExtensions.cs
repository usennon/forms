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

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

        public static void ConfigureSqlContext(this IServiceCollection services, string? connectionString) =>
            services.AddDbContextPool<FormsDbContext>(options => options.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
    }
}
