using InfoTrack.SEOTracker.Services.Interfaces;
using InfoTrack.SEOTracker.Utilities.Appsettings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Mongo2Go;
using Moq;

namespace InfoTrack.SEOTracker.Api.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
   private readonly MongoDbRunner _runner;

   public CustomWebApplicationFactory()
   {
      _runner = MongoDbRunner.Start();
   }
   protected override void ConfigureWebHost(IWebHostBuilder builder)
   {
      builder.UseEnvironment("test");
      Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "test");

      builder.ConfigureServices(services =>
      {
         // Replace MongoDbConfig with Mongo2Go connection string
         var options = Options.Create(new MongoDbConfig()
         {
            TempConnection = _runner.ConnectionString,
            DatabaseName = "TestDatabase"
         });


         // Remove existing IOptions<MongoDbConfig> service descriptor
         services.RemoveAll<IOptions<MongoDbConfig>>();

         // Add the new IOptions<MongoDbConfig> service descriptor
         services.AddSingleton(options);

         var htmlRenderService = new Mock<IHtmlRenderService>();

         htmlRenderService.Setup(x => x.GetGoogleHtmlContentAsync(It.IsAny<string>()))
         .ReturnsAsync(@"<ul>
                          <li><cite class=""tjvcx GvPZzd cHaqb"" role=""text"">https://propertychecker.co.uk</cite></li>
                          <li><cite class=""tjvcx GvPZzd cHaqb"" role=""text"">https://mockpropertyvalue.com</cite></li>
                          <li><cite class=""tjvcx GvPZzd cHaqb"" role=""text"">https://infotrack.co.uk</cite></li>
                          <li><cite class=""tjvcx GvPZzd cHaqb"" role=""text"">https://example-realestate.net</cite></li>
                          <li><cite class=""tjvcx GvPZzd cHaqb"" role=""text"">https://yourhomecheck.io</cite></li>
                          <li><cite class=""tjvcx GvPZzd cHaqb"" role=""text"">https://fakeurl.uk/test</cite></li>
                        </ul>");
         services.RemoveAll<IHtmlRenderService>();
         services.AddScoped((service) => htmlRenderService.Object);

      });
   }

   protected override void Dispose(bool disposing)
   {
      _runner.Dispose();
      base.Dispose(disposing);
   }
}