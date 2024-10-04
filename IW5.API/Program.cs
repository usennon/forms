
using IW5.DAL;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Add DbContext and Repositories
            var connectionString = configuration.GetConnectionString("IW5");
            builder.Services.AddDbContextPool<FormsDbContext>(
                options => options.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure()));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IFormRepository, FormRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IOptionRepository, OptionRepository>();

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
