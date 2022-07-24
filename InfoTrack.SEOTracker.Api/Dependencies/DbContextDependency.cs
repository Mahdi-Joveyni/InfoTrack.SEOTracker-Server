using InfoTrack.SEOTracker.Data;
using InfoTrack.SEOTracker.Domain.Appsettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SEOTracker.Api.Dependencies
{
   public static class DbContextDependency
   {
      public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
      {
         var databaseSetting = configuration.GetSection(DatabaseSetting.Position).Get<DatabaseSetting>();
         var assembly = typeof(SEOTrackerDBContext).Assembly.GetName().Name;

         services.AddDbContext<SEOTrackerDBContext>(
             options => options.UseSqlServer(databaseSetting.ConnectionString, o => o.MigrationsAssembly(assembly)));
      }
   }
}
