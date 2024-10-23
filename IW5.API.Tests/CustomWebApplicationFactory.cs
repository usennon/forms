using System.Reflection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IW5.API.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(collection =>
            {
                // Load controller assemblies
                var controllerAssemblyName = typeof(Program).Assembly.FullName;
                collection.AddMvc().AddApplicationPart(Assembly.Load(controllerAssemblyName!));
            });
            return base.CreateHost(builder);
        }
    }
}
