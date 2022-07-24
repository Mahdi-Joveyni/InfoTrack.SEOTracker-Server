using InfoTrack.SEOTracker.Data.Repositories;
using InfoTrack.SEOTracker.Data.Repositories.Interfaces;
using InfoTrack.SEOTracker.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SEOTracker.Api.Dependencies
{
   public static class ServiceDependency
   {
      public static void RegisterServices(this IServiceCollection services)
      {
         services.AddScoped<GoogleService>();
         services.AddScoped<ITrackerRepository, TrackerRepository>();
      }
   }
}
