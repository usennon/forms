using IW5.Common.Installers;
using IW5.IdentityProvider.DAL.Entities;
using IW5.IdentityProvider.DAL;
using Microsoft.AspNetCore.Identity;

namespace IW5.IdentityProvider.App.Installers;

public class IdentityProviderAppInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<AppUserEntity, AppRoleEntity>()
            .AddEntityFrameworkStores<IdentityProviderDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<AppUserEntity>>(TokenOptions.DefaultProvider);

        serviceCollection.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 2;

            options.SignIn.RequireConfirmedEmail = false;
        });
    }
}
