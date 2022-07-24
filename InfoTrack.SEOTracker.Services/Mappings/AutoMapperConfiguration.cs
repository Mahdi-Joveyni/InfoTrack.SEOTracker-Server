using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.SEOTracker.Services.Mappings
{
   public static class AutoMapperConfiguration
   {
      public static IMapper RegisterMapper(IServiceCollection services)
      {

         var mapperConfig = new MapperConfiguration(mc =>
         {
            mc.AddProfile(new TrackerProfile());
         });

         return mapperConfig.CreateMapper();
      }
   }
}