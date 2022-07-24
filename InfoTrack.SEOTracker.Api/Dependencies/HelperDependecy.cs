using AutoMapper;
using InfoTrack.SEOTracker.Services.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SEOTracker.Api.Dependencies
{
   public static class HelperDependecy
   {
      public static void RegisterHelper(this IServiceCollection services)
      {
         IMapper mapper = AutoMapperConfiguration.RegisterMapper(services);
         services.AddSingleton(mapper);
      }
   }
}