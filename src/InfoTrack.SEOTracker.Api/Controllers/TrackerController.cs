using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Domain.DTO;
using InfoTrack.SEOTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SEOTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrackerController(ITrackerService trackerService) : ControllerBase
{
   [HttpGet]
   [ProducesResponseType(typeof(DataTableResult<TrackerDto>), 200)]
   public async Task<IActionResult> GetAllTracker([FromQuery] DataTableRequest request, CancellationToken cancellationToken)
   {
      return Ok(await trackerService.GetAllTracker(request, cancellationToken));
   }

   [HttpGet("history")]
   [ProducesResponseType(typeof(TrackerDto), 200)]
   public async Task<IActionResult> GetByKey(string key, CancellationToken cancellationToken)
   {
      return Ok(await trackerService.GetByKey(key, cancellationToken));
   }


   [HttpGet("google")]
   public async Task<IActionResult> GetGoogleRank([FromServices] IGoogleService googleService, string search, string url)
   {
      return Ok(await googleService.GetRank(search, url));
   }
}
