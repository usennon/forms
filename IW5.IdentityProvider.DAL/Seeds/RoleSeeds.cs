using IW5.IdentityProvider.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.IdentityProvider.DAL.Seeds
{
    public static class RoleSeeds
    {
        public static readonly AppUserClaimEntity adminClaim = new()
        {
            Id = 1,
            UserId = UserSeeds.owner.Id,
            ClaimType = "role",
            ClaimValue = "admin"
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<AppUserClaimEntity>().HasData(adminClaim);
    }
}
