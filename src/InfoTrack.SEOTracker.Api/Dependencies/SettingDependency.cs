using InfoTrack.SEOTracker.Utilities.Appsettings;
using InfoTrack.SEOTracker.Utilities.Helpers;

namespace InfoTrack.SEOTracker.Api.Dependencies;

public static class SettingDependency
{
   public static AppSetting RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
   {
      var appsetting = configuration.GetSection(AppSetting.Position);
      EncryptionHelper.Key = appsetting.Key;
      services.Configure<AppSetting>(appsetting);
      return appsetting.Get<AppSetting>()!;
   }
}
