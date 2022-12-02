using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

//builder.Host
//    .ConfigureAppConfiguration((hostingContext, config) =>
//    {
//        config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
//    })
//    .ConfigureLogging((hostingContext, logging) =>
//    {
//        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//        logging.AddConsole();
//        logging.AddDebug();
//    });

builder.Services.AddOcelot()
    .AddCacheManager(settings =>
    {
        settings.WithDictionaryHandle();
    });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
