using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Domain.DTO;
using InfoTrack.SEOTracker.Domain.Enumerations;
using InfoTrack.SEOTracker.Services.Interfaces;
using InfoTrack.SEOTracker.Utilities.Appsettings;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace InfoTrack.SEOTracker.Services;

public class GoogleService(IHtmlRenderService htmlRenderService, IOptions<AppSetting> appSetting,
   IMongoRepository<Tracker> trackerRepository)
   : IGoogleService
{
   private readonly AppSetting _appSetting = appSetting.Value;
   private readonly string _googleAddress = "https://www.google.co.uk/search?q={0}&num={1}";

   public async Task<List<int>> GetRank(string search, string url, CancellationToken cancellationToken = default)
   {

      var finalHtml = await htmlRenderService.GetGoogleHtmlContentAsync(string.Format(_googleAddress, HttpUtility.UrlEncode(search), _appSetting.PageSize));

      List<int> result = FindRank(url, finalHtml);

      var tracker = await trackerRepository.FindOneAsync(x =>
         x.Url.Equals(url, StringComparison.CurrentCultureIgnoreCase)
      && x.Search.Equals(search, StringComparison.CurrentCultureIgnoreCase)
      && x.EngineType == EngineType.Google, cancellationToken);

      if (tracker == null)
      {
         tracker = await trackerRepository.InsertOneAsync(new()
         {
            EngineType = EngineType.Google,
            Search = search,
            Url = url,
            Histories = [new() { Ranks = result }]
         },
            cancellationToken);
         return result;
      }

      tracker.Histories.Add(new()
      {
         Ranks = result
      });
      await trackerRepository.ReplaceOneAsync(tracker, cancellationToken);
      return result;
   }

   private static List<int> FindRank(string url, string finalHtml)
   {
      string pattern = @"<cite[^>]*>(.*?)<\/cite>";
      var result = new List<int>();
      int count = 1;
      string oldSelectedAnchor = string.Empty;

      foreach (Match match in Regex.Matches(finalHtml, pattern, RegexOptions.IgnoreCase))
      {
         // Get the full <cite>...</cite> block
         int startIndex = match.Index;
         int endIndex = finalHtml.IndexOf("</cite>", startIndex, StringComparison.OrdinalIgnoreCase);

         if (endIndex == -1) continue; // safety check in case </cite> not found

         string selectedAnchor = finalHtml.Substring(startIndex, endIndex - startIndex + "</cite>".Length);

         // Check for duplicates
         if (!string.Equals(selectedAnchor, oldSelectedAnchor, StringComparison.OrdinalIgnoreCase))
         {
            if (selectedAnchor.Contains(url, StringComparison.CurrentCultureIgnoreCase))
            {
               result.Add(count);
            }

            count++;
            oldSelectedAnchor = selectedAnchor;
         }
      }

      return result;
   }
}