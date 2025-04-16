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
         SlowMo = 200      // Slow things down a bit
      });
      var context = await browser.NewContextAsync(new BrowserNewContextOptions
      {
         ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
         UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123 Safari/537.36",
         TimezoneId = "Europe/London",
         Locale = "en-US"
      });


      var page = await context.NewPageAsync();

      await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

      await page.WaitForLoadStateAsync(LoadState.NetworkIdle);


      // need to approve you are not a robot
      // chromium will be popup and ask you to approve
      while (!page.IsClosed && (await page.ContentAsync()).Contains("detected unusual traffic"))
      {
         await Task.Delay(2000);
      }


      string htmlContent = await page.ContentAsync();
      return htmlContent;
   }
}
