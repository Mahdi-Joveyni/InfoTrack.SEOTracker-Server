using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Services.Interfaces
{
   public interface ITrackerService
   {
      Task<List<int>> GetRank(string search, string url);
   }
}