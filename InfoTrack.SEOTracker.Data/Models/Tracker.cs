using InfoTrack.SEOTracker.Domain.Enumerations;
using System.Collections.Generic;

namespace InfoTrack.SEOTracker.Data.Models
{
   public class Tracker
   {
      public int Id { get; set; }
      public string Search { get; set; }
      public string Url { get; set; }
      public EngineType EngineType { get; set; }

      public ICollection<TrackerHistory> Histories { get; set; }
   }
}
