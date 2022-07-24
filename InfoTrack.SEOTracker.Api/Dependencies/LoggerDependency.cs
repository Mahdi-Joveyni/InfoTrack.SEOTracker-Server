using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;

namespace InfoTrack.SEOTracker.Api.Dependencies
{
   public static class LoggerDependency
   {
      public static Serilog.ILogger RegisterLogger(this IServiceCollection services, IConfiguration configuration)
      {
         var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower();



         Serilog.ILogger logger;
         if (environment == "development")
         {
            logger = new LoggerConfiguration()
                     .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch() { MinimumLevel = LogEventLevel.Debug })
                     .WriteTo.Console(outputTemplate: "{Name} [{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                     .MinimumLevel.Override("System", LogEventLevel.Warning)
                     //.WriteTo.Http(requestUri: logSetting.LogUrl, httpClient: new SinkHttpClient(logSetting.ApiKey))
                     //.Enrich.WithProperty(LogApplicationSetting.Position, logSetting)
                     .CreateLogger();
         }
         else
         {
            logger = new LoggerConfiguration()
                    .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch() { MinimumLevel = LogEventLevel.Debug })
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                     .MinimumLevel.Override("System", LogEventLevel.Warning)
                     .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
         }
         services.AddSingleton(sp => logger);
         services.AddLogging(cfg =>
         {
            object value = cfg.ClearProviders().AddSerilog();
         });
         return logger;
      }
   }
}
