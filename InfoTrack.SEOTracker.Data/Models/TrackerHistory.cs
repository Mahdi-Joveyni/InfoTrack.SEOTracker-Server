using System;

namespace InfoTrack.SEOTracker.Data.Models
{
   public class TrackerHistory
   {
      public int Id { get; set; }
      public string Ranks { get; set; }
      public DateTime InsertDate { get; set; }

      public int TrackerId { get; set; }

      public Tracker Tracker { get; set; }
   }
}
