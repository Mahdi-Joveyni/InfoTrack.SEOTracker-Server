using InfoTrack.SEOTracker.Data.Repositories.Interfaces;
using InfoTrack.SEOTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TrackerController : ControllerBase
   {
      private readonly ILogger _logger;

      public TrackerController(ILogger logger)
      {
         _logger = logger;
      }

      [HttpGet]
      public async Task<IActionResult> Get([FromServices] ITrackerRepository trackerRepostory)
      {
         return Ok(await trackerRepostory.GetTrackers());
      }

      [HttpGet("history")]
      public async Task<IActionResult> GetAllHistory([FromServices] ITrackerRepository trackerRepostory, int id)
      {
         return Ok(await trackerRepostory.GetTrackerByHistories(id));
      }


      [HttpGet("google")]
      public async Task<IActionResult> GetGoogleRank([FromServices] GoogleService googleService, string search, string url)
      {
         return Ok(await googleService.GetRank(search, url));
      }

   }

}
