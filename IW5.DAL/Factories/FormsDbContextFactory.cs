using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IW5.DAL.Factories
{
    public class FormsDbContextFactory : IDesignTimeDbContextFactory<FormsDbContext>
    {
        public FormsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<FormsDbContextFactory>(optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FormsDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:sqldb-iw5-2024-team-xpopov10.database.windows.net,1433;Initial Catalog=db-iw5-2024-team-xpopov10;Persist Security Info=False;User ID=xpopov10;Password=/Automation#567*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=100;");
            return new FormsDbContext(optionsBuilder.Options);
        }
    }
}
