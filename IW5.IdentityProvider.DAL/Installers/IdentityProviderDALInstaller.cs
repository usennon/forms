using IW5.IdentityProvider.DAL.Repositories;
using IW5.IdentityProvider.DAL.Entities;
using IW5.IdentityProvider.DAL.Factories;
using IW5.IdentityProvider.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IW5.Common.Installers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IW5.Models.Entities;

namespace IW5.IdentityProvider.DAL.Installers;

public class IdentityProviderDALInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextFactory<IdentityProviderDbContext>, IdentityProviderDbContextFactory>();

        serviceCollection.AddScoped<IUserStore<User>, UserStore<User, AppRoleEntity, IdentityProviderDbContext, Guid, AppUserClaimEntity, AppUserRoleEntity, AppUserLoginEntity, AppUserTokenEntity, AppRoleClaimEntity>>();
        serviceCollection.AddScoped<IRoleStore<AppRoleEntity>, RoleStore<AppRoleEntity, IdentityProviderDbContext, Guid, AppUserRoleEntity, AppRoleClaimEntity>>();

        serviceCollection.AddTransient<IAppUserRepository, AppUserRepository>();

        serviceCollection.AddTransient(serviceProvider =>
        {
            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<IdentityProviderDbContext>>();
            return dbContextFactory.CreateDbContext();
        });
    }
}
