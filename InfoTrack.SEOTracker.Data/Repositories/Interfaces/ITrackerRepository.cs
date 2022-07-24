using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Data.Repositories.Interfaces
{
   public interface ITrackerRepository
   {
      Task AddNewTracker(Tracker request, List<int> ranks);
      Task<List<TrackerDto>> GetTrackers();
      Task<Tracker> GetTrackerByHistories(int trackerId);
   }
}