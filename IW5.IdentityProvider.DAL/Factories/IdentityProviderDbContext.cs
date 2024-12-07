using IW5.IdentityProvider.DAL.Entities;
using IW5.IdentityProvider.DAL.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IW5.IdentityProvider.DAL;

public class IdentityProviderDbContext : IdentityDbContext<AppUserEntity, AppRoleEntity, Guid, AppUserClaimEntity, AppUserRoleEntity, AppUserLoginEntity, AppRoleClaimEntity, AppUserTokenEntity>
{
    public IdentityProviderDbContext(DbContextOptions options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        UserSeeds.Seed(builder);
        RoleSeeds.Seed(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}