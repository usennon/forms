using Microsoft.Extensions.DependencyInjection;
namespace IW5.Common.Installers
{
    
        public interface IInstaller
        {
            void Install(IServiceCollection serviceCollection);
        }

}
