using InfoTrack.SEOTracker.Data.Models.Base;
using InfoTrack.SEOTracker.Domain.DTO;
using InfoTrack.SEOTracker.Domain.Enumerations;

namespace InfoTrack.SEOTracker.Data.Models;

public class Tracker : BaseModel
{
   public string Search { get; set; } = string.Empty;
   public string Url { get; set; } = string.Empty;
   public EngineType EngineType { get; set; }
   public List<TrackerHistory> Histories { get; set; } = [];
}
