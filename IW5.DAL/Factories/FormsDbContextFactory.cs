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
            optionsBuilder.UseSqlServer("Server=DESKTOP-UR6RKLM\\ALBERT;Database=IW5;Trusted_Connection=True;TrustServerCertificate=True");
            return new FormsDbContext(optionsBuilder.Options);
        }
    }
}
