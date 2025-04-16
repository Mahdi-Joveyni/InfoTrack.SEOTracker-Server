using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Domain.Constants;
using InfoTrack.SEOTracker.Domain.DTO;
using InfoTrack.SEOTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SEOTracker.Api.Controllers;

[Route($"api/{RouteConstant.Tracker}")]
[ApiController]
public class TrackerController(ITrackerService trackerService) : ControllerBase
{
   [HttpGet]
   [ProducesResponseType(typeof(DataTableResult<TrackerDto>), 200)]
   public async Task<IActionResult> GetAllTracker([FromQuery] DataTableRequest request, CancellationToken cancellationToken)
   {
      return Ok(await trackerService.GetAllTracker(request, cancellationToken));
   }

   [HttpGet(RouteConstant.History)]
   [ProducesResponseType(typeof(TrackerDto), 200)]
   public async Task<IActionResult> GetByKey(string key, CancellationToken cancellationToken)
   {
      return Ok(await trackerService.GetByKey(key, cancellationToken));
   }


   [HttpGet(RouteConstant.Google)]
   [ProducesResponseType(typeof(List<int>), 200)]
   public async Task<IActionResult> GetGoogleRank([FromServices] IGoogleService googleService, string search, string url)
   {
      return Ok(await googleService.GetRank(search, url));
   }
}
