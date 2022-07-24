using InfoTrack.SEOTracker.Data.Repositories.Interfaces;
using InfoTrack.SEOTracker.Domain.Appsettings;
using InfoTrack.SEOTracker.Domain.Enumerations;
using InfoTrack.SEOTracker.Services.Helpers;
using InfoTrack.SEOTracker.Services.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace InfoTrack.SEOTracker.Services
{
   public class GoogleService : ITrackerService
   {
      private readonly ILogger _logger;
      private readonly ITrackerRepository _trackerRepository;
      private readonly AppSetting _appSetting;
      private readonly string _googleAddress = "https://www.google.co.uk/";

      public GoogleService(ILogger logger, IOptions<AppSetting> appSetting, ITrackerRepository trackerRepository)
      {
         _logger = logger;
         _trackerRepository = trackerRepository;
         _appSetting = appSetting.Value;
      }

      public async Task<List<int>> GetRank(string search, string url)
      {

         var finalHtml = await FetchPageHelper.GetPage($"{_googleAddress}search?q={HttpUtility.UrlEncode(search)}&num={_appSetting.PageSize}");

         var anchorPattern = "<a href=\"http";
         var h3Pattern = "<h3";
         var result = new List<int>();
         string selectedAnchor;
         int count = 1;
         foreach (Match match in Regex.Matches(finalHtml, anchorPattern, RegexOptions.IgnoreCase))
         {
            selectedAnchor = finalHtml.Substring(match.Index, 400);
            if (Regex.IsMatch(selectedAnchor, h3Pattern, RegexOptions.IgnoreCase))
            {
               if (Regex.IsMatch(selectedAnchor, url, RegexOptions.IgnoreCase))
               {
                  result.Add(count);
               }
               count++;
            }
         }
         await _trackerRepository.AddNewTracker(new() { EngineType = EngineType.Google, Search = search, Url = url }, result);
         return result;
      }

   }
}
