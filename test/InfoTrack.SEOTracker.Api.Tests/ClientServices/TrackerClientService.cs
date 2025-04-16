using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Domain.Constants;
using InfoTrack.SEOTracker.Domain.DTO;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace InfoTrack.SEOTracker.Api.Tests.ClientServices;

public class TrackerClientService(HttpClient httpClient)
{
   private readonly string ApiPrefix = "api";
   public async Task<DataTableResult<TrackerDto>> GetAllTracker(DataTableRequest request, CancellationToken cancellationToken = default)
   {
      var url = QueryHelpers.AddQueryString(GetActionPath(""), request.ToDictionary());
      return await httpClient.GetFromJsonAsync<DataTableResult<TrackerDto>>(url, cancellationToken) ?? new DataTableResult<TrackerDto>();
   }

   public async Task<TrackerDto?> GetByKey(string key, CancellationToken cancellationToken = default)
   {
      var url = QueryHelpers.AddQueryString(GetActionPath(RouteConstant.History), new Dictionary<string, string?>() { { nameof(key), key } });
      return await httpClient.GetFromJsonAsync<TrackerDto?>(url, cancellationToken);
   }

   public async Task<List<int>> GetGoogleRank(string search, string url, CancellationToken cancellationToken = default)
   {
      var actionUrl = QueryHelpers.AddQueryString(
         GetActionPath(RouteConstant.Google),
         new Dictionary<string, string?>() { { nameof(search), search },
         { nameof(url), url }});
      return (await httpClient.GetFromJsonAsync<List<int>>(actionUrl, cancellationToken)) ?? [];
   }


   public string GetActionPath(string action)
   {
      if (string.IsNullOrEmpty(action))
         return $"{ApiPrefix}/{RouteConstant.Tracker}";
      return $"{ApiPrefix}/{RouteConstant.Tracker}/{action}";
   }
}
