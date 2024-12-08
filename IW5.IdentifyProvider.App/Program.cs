using AutoMapper;
using IW5.IdentityProvider.App.Installers;
using IW5.IdentityProvider.App;
using IW5.IdentityProvider.App.Endpoints;
using IW5.IdentityProvider.BL.Installers;
using IW5.IdentityProvider.DAL.Installers;
using IW5.Common.Installers;
using Serilog;
using Microsoft.EntityFrameworkCore.SqlServer;
using Duende.IdentityServer.Models;
using IW5.IdentityProvider.DAL;
using Microsoft.EntityFrameworkCore;
using IW5.DAL.Initialization;
using IW5.DAL;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddInstaller<IdentityProviderDALInstaller>();
    builder.Services.AddInstaller<IdentityProviderBLInstaller>();
    builder.Services.AddInstaller<IdentityProviderAppInstaller>();
    var configuration = builder.Configuration;

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));


    builder.Services.AddDbContextPool<IdentityProviderDbContext>(options => options.UseSqlServer("Server=DESKTOP-UR6RKLM\\ALBERT;Database=IW5;Trusted_Connection=True;TrustServerCertificate=True",
            sqlOptions => sqlOptions.EnableRetryOnFailure()));

    var connectionString = configuration.GetConnectionString("IW5");


    var app = builder.ConfigureServices();
    //if (configuration.GetValue<bool>("RebuildDataBase"))
    //{
    //    SampleDataInitializer.InitializeData(new IdentityProviderDbContext(
    //                               new DbContextOptionsBuilder<IdentityProviderDbContext>()
    //                                                          .UseSqlServer(connectionString).Options));
    //}

    var mapper = app.Services.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();

    app.ConfigurePipeline();

    app.MapGroup("api")
        .AllowAnonymous()
        .UseUserEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
