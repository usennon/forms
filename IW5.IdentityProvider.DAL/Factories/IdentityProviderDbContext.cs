using IW5.IdentityProvider.DAL.Entities;
using IW5.IdentityProvider.DAL.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using IW5.Models;
using IW5.Models.Entities;
using System.Reflection.Emit;

namespace IW5.IdentityProvider.DAL;

public class IdentityProviderDbContext : IdentityDbContext<User, AppRoleEntity, Guid, AppUserClaimEntity, AppUserRoleEntity, AppUserLoginEntity, AppRoleClaimEntity, AppUserTokenEntity>
{
    public IdentityProviderDbContext(DbContextOptions options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>(entity => {
            entity.ToTable("Users");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }


}