using IW5.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace IW5.DAL.Tests
{
    public static class TestHelpers
    {
        public static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        public static FormsDbContext GetContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FormsDbContext>();
            var connectionString = configuration.GetConnectionString("IW5");
            optionsBuilder.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
            return new FormsDbContext(optionsBuilder.Options);
        }

        public static FormsDbContext GetSecondContext(FormsDbContext oldContext,
            IDbContextTransaction trans)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FormsDbContext>();
            optionsBuilder.UseSqlServer(
                oldContext.Database.GetDbConnection(),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            var context = new FormsDbContext(optionsBuilder.Options);
            context.Database.UseTransaction(trans.GetDbTransaction());
            return context;
        }
    }
}