using IW5.BL.API.Contracts;
using IW5.Common.Installers;
using IW5.IdentityProvider.BL.MapperProfiles;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Formats.Tar;

namespace IW5.IdentityProvider.BL.Installers;

public class IdentityProviderBLInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(AppUserMapperProfile));
        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<IdentityProviderBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<ILogic>())
                .AsSelfWithInterfaces()
                .WithScopedLifetime());
    }
}
