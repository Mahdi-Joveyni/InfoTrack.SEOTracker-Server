using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Domain.DTO;
using InfoTrack.SEOTracker.Services.Interfaces;
using InfoTrack.SEOTracker.Utilities.Helpers;

namespace InfoTrack.SEOTracker.Services;

public class TrackerService(IMongoRepository<Tracker> trackerRepository) : ITrackerService
{
   public async Task<DataTableResult<TrackerDto>> GetAllTracker(DataTableRequest dataTableRequest, CancellationToken cancellationToken = default)
   {
      var trackerResult = await trackerRepository.FilterByAsync(_ => true, dataTableRequest, cancellationToken);
      return new DataTableResult<TrackerDto>
      {
         Items = [.. trackerResult.Items.Select(GetDto)],
         TotalCount = trackerResult.TotalCount,
      };
   }

   public async Task<TrackerDto?> GetByKey(string key, CancellationToken cancellationToken = default)
   {
      var tracker = await trackerRepository.FindByIdAsync(key.ToObjectId(), cancellationToken);
      if (tracker == null)
         return null;

      return GetDto(tracker);
   }

   private static TrackerDto GetDto(Tracker tracker)
   {
      return new TrackerDto
      {
         Key = tracker.Id.ToUrlFromObjectId(),
         Search = tracker.Search,
         Url = tracker.Url,
         EngineType = tracker.EngineType,
         Histories = [.. tracker.Histories.OrderByDescending(x => x.CreateDateTime)]
      };
   }
}
