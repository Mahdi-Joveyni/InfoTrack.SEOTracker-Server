namespace InfoTrack.SEOTracker.Api.Tests;

public class ControllerTestBase<TStartup> : IDisposable where TStartup : class
{
   public HttpClient LocalHttpClient;
   public readonly CustomWebApplicationFactory<TStartup> _webApplicationFactory;

   public ControllerTestBase(CustomWebApplicationFactory<TStartup> webApplicationFactory)
   {
      _webApplicationFactory = webApplicationFactory;
      LocalHttpClient = _webApplicationFactory.CreateClient();
   }

   public virtual void Dispose()
   {
      LocalHttpClient.Dispose();
      _webApplicationFactory.Dispose();
      GC.SuppressFinalize(this);
   }
}
