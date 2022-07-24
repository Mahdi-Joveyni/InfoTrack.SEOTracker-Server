using InfoTrack.SEOTracker.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Domain.DTO
{
   public class TrackerDto
   {
      public int Id { get; set; }
      public string Search { get; set; }
      public string Url { get; set; }
      public EngineType EngineType { get; set; }

      public ICollection<TrackerHistoryDto> Histories { get; set; }
   }
}
