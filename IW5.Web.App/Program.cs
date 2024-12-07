using IW5.Web.App;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");
builder.Services.AddHttpClient("iw5api", client => client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler(serviceProvider
    => serviceProvider?.GetService<AuthorizationMessageHandler>()
        ?.ConfigureHandler(
            authorizedUrls: new[] { apiBaseUrl },
            scopes: new[] { "iw5api" }));
builder.Services.AddScoped<HttpClient>(serviceProvider => serviceProvider.GetService<IHttpClientFactory>().CreateClient("iw5api"));

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<AppConfig>("appsettings.json");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });


// Register the API service
builder.Services.AddScoped<IW5ApiService>();
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("IdentityServer", options.ProviderOptions);
    options.ProviderOptions.Authority = "https://localhost:5002";
    options.ProviderOptions.ClientId = "iw5client";
    options.ProviderOptions.RedirectUri = "https://localhost:5001/authentication/login-callback";
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();

class AppConfig
{
    public string ApiBaseUrl { get; set; }
}