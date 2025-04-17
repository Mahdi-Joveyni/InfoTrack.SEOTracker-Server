using InfoTrack.SEOTracker.Services.Interfaces;
using Microsoft.Playwright;

namespace InfoTrack.SEOTracker.Services;

public class HtmlRenderService : IHtmlRenderService
{
   public async Task<string> GetGoogleHtmlContentAsync(string url)
   {
      using var playwright = await Playwright.CreateAsync();
      await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
      {

         Headless = false,
      });
      var context = await browser.NewContextAsync(new BrowserNewContextOptions
      {
         UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123 Safari/537.36",
      });


      var page = await context.NewPageAsync();

      await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

      await page.WaitForLoadStateAsync(LoadState.NetworkIdle);


      // need to approve you are not a robot
      // chromium will be popup and ask you to approve
      // it's not for production, just for testing and POC
      while (!page.IsClosed && (await page.ContentAsync()).Contains("detected unusual traffic"))
      {
         await Task.Delay(2000);
      }


      return await page.ContentAsync();
   }
}
