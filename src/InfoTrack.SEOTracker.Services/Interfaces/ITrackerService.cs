using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Domain.DTO;

namespace InfoTrack.SEOTracker.Services.Interfaces
{
   public interface ITrackerService
   {
      Task<DataTableResult<TrackerDto>> GetAllTracker(DataTableRequest dataTableRequest, CancellationToken cancellationToken = default);
      Task<TrackerDto?> GetByKey(string key, CancellationToken cancellationToken = default);
   }
}