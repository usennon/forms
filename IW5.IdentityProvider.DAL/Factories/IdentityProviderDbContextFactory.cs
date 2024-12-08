using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IW5.IdentityProvider.DAL.Factories;

public class IdentityProviderDbContextFactory : IDesignTimeDbContextFactory<IdentityProviderDbContext>, IDbContextFactory<IdentityProviderDbContext>
{
    private readonly Assembly startupAssembly;

    public IdentityProviderDbContextFactory()
    {
        startupAssembly = Assembly.GetEntryAssembly()!;
    }

    public IdentityProviderDbContext CreateDbContext(string[] args)
        => CreateDbContext();

    public IdentityProviderDbContext CreateDbContext()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
#if DEBUG
            .AddJsonFile("appsettings.Development.json", optional: true)
#endif
            .AddUserSecrets<IdentityProviderDbContextFactory>(optional: true)
            .AddUserSecrets(startupAssembly, optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<IdentityProviderDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=iw5_auth;Trusted_Connection=True;TrustServerCertificate=True");
        return new IdentityProviderDbContext(optionsBuilder.Options);
    }
}
