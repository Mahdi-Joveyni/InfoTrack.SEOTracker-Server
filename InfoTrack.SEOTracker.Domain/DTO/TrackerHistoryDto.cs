using System;

namespace InfoTrack.SEOTracker.Domain.DTO
{
   public class TrackerHistoryDto
   {
      public int Id { get; set; }
      public string Ranks { get; set; }
      public DateTime InsertDate { get; set; }
   }
}
