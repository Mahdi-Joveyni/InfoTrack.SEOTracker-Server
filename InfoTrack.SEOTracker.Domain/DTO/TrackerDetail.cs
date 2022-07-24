using System;

namespace InfoTrack.SEOTracker.Domain.DTO
{
   public class TrackerDetail
   {
      public int Id { get; set; }
      public string Search { get; set; }
      public string Url { get; set; }
      public string LastRank { get; set; }
      public DateTime LastDateTime { get; set; }
   }
}
