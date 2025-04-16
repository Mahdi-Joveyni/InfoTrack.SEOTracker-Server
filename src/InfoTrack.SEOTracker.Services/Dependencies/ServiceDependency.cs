using InfoTrack.SEOTracker.Data.Dependencies;
using InfoTrack.SEOTracker.Services.Interfaces;
using InfoTrack.SEOTracker.Utilities.Appsettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace InfoTrack.SEOTracker.Services.Dependencies;

public static class ServiceDependency
{
   public static void AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
   {
      Log.Logger = new LoggerConfiguration()
             .MinimumLevel.ControlledBy(new()
             {
                MinimumLevel = LogEventLevel.Debug
             })
          .WriteTo.Console(outputTemplate: "{Name} [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
          .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
          .MinimumLevel.Override("System", LogEventLevel.Warning)
          .CreateLogger();

      services.AddHttpClient();
      services.Configure<MongoDbConfig>(configuration.GetSection(MongoDbConfig.SectionName));

      services.AddDataDependency();
      services.AddScoped<ITrackerService, TrackerService>();
      services.AddScoped<IGoogleService, GoogleService>();
      services.AddScoped<IHtmlRenderService, HtmlRenderService>();

      Microsoft.Playwright.Program.Main(["install"]);

   }
}
