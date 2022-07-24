using InfoTrack.SEOTracker.Domain.Appsettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SEOTracker.Api.Dependencies
{
   public static class SettingDependency
   {
      public static AppSetting RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
      {
         var appsetting = configuration.GetSection(AppSetting.Position);
         services.Configure<AppSetting>(appsetting);
         return appsetting.Get<AppSetting>();
      }
   }
}
