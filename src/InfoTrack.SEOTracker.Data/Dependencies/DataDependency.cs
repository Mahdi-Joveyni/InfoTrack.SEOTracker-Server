using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SEOTracker.Data.Dependencies;
public static class DataDependency
{
   public static void AddDataDependency(this IServiceCollection services)
   {
      services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
   }
}
