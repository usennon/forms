using IW5.IdentityProvider.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.IdentityProvider.DAL.Seeds
{
    public static class UserSeeds
    {
        public static readonly AppUserEntity owner = new AppUserEntity()
        {
            Id = Guid.Parse("965dc8a8-63a2-448d-86c2-101632acfef3"),
            UserName = "Albert",
            Subject = "Albert",
            NormalizedUserName = "ALBERT",
            Email = "xpopov10@vutbr.cz",
            NormalizedEmail = "XPOPOV10@VUTBR.CZ",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEEOkrEig4UgwbphGvgETx6+h77MErt3yqSNYQyl4dpCrY+WEpB4/Fn+gFIDg7rr9qQ==",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false
        };

        public static readonly AppUserEntity prostoVlad = new AppUserEntity()
        {
            Id = Guid.Parse("965dc8a8-63a2-448d-86c2-101632acfef4"),
            UserName = "Vlad",
            Subject = "Vlad",
            NormalizedUserName = "VLAD",
            Email = "xmalas04@vutbr.cz",
            NormalizedEmail = "XMALAS04@VUTBR.CZ",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEEOkrEig4UgwbphGvgETx6+h77MErt3yqSNYQyl4dpCrY+WEpB4/Fn+gFIDg7rr9qQ==",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = false
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<AppUserEntity>().HasData(owner, prostoVlad);
    }
}
