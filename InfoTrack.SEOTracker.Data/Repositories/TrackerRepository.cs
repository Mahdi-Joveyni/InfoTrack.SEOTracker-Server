using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Data.Repositories.Interfaces;
using InfoTrack.SEOTracker.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Data.Repositories
{
   public class TrackerRepository : ITrackerRepository
   {
      private readonly SEOTrackerDBContext _dBContext;

      public TrackerRepository(SEOTrackerDBContext dBContext)
      {
         _dBContext = dBContext;
      }

      public async Task AddNewTracker(Tracker request, List<int> ranks)
      {
         var tracker = await _dBContext.Trackers.Include(t => t.Histories).SingleOrDefaultAsync(t => t.Search.ToLower() == request.Search.ToLower() && t.Url.ToLower() == request.Url.ToLower() && t.EngineType == request.EngineType);

         if (tracker is null)
         {
            tracker = request;
            tracker.Histories = new List<TrackerHistory>();
            _dBContext.Trackers.Add(tracker);
         }
         tracker.Histories.Add(new TrackerHistory() { InsertDate = DateTime.Now, Ranks = String.Join(",", ranks) });
         await _dBContext.SaveChangesAsync();
      }


      public async Task<List<TrackerDetail>> GetTrackers()
      {
         // To-Do add Pagination
         return await _dBContext.Trackers.Include(t => t.Histories).Select(t => new TrackerDetail()
         {
            Id = t.Id,
            Search = t.Search,
            Url = t.Url,
            LastRank = t.Histories.OrderBy(t => t.InsertDate).LastOrDefault()!.Ranks,
            LastDateTime = t.Histories.OrderBy(t => t.InsertDate).LastOrDefault()!.InsertDate
         }).OrderByDescending(t => t.LastDateTime).ToListAsync();
      }

      public async Task<Tracker> GetTrackerByHistories(int trackerId)
      {
         return await _dBContext.Trackers.Include(t => t.Histories).Select(t => new Tracker()
         {
            Id = t.Id,
            Search = t.Search,
            Url = t.Url,
            Histories = t.Histories.OrderByDescending(t => t.InsertDate).ToList()
         }).SingleOrDefaultAsync(t => t.Id == trackerId);
      }
   }
}
