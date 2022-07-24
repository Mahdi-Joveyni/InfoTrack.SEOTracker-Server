using AutoMapper;
using InfoTrack.SEOTracker.Data.Repositories.Interfaces;
using InfoTrack.SEOTracker.Domain.DTO;
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
      private readonly IMapper _mapper;

      public TrackerController(ILogger logger, IMapper mapper)
      {
         _logger = logger;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<IActionResult> Get([FromServices] ITrackerRepository trackerRepostory)
      {
         return Ok(await trackerRepostory.GetTrackers());
      }

      [HttpGet("history")]
      public async Task<IActionResult> GetAllHistory([FromServices] ITrackerRepository trackerRepostory, int id)
      {
         return Ok(_mapper.Map<TrackerDto>(await trackerRepostory.GetTrackerByHistories(id)));
      }


      [HttpGet("google")]
      public async Task<IActionResult> GetGoogleRank([FromServices] GoogleService googleService, string search, string url)
      {
         return Ok(await googleService.GetRank(search, url));
      }

   }

}
