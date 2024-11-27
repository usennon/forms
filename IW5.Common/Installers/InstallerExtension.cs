using Microsoft.Extensions.DependencyInjection;


namespace IW5.Common.Installers
{
    public static class InstallerExtension
    {
        public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection)
            where TInstaller : IInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(serviceCollection);
        }
    }
}
