namespace InfoTrack.SEOTracker.Services.Interfaces
{
   public interface IHtmlRenderService
   {
      //Task<string> GetGoogleSearchHtmlAsync(string keyword, int pageSize);
      Task<string> GetGoogleHtmlContentAsync(string url);
   }
}