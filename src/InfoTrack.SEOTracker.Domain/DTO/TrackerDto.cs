using InfoTrack.SEOTracker.Domain.Enumerations;

namespace InfoTrack.SEOTracker.Domain.DTO;

public class TrackerDto
{
   public string Key { get; set; } = string.Empty;
   public string Search { get; set; } = string.Empty;
   public string Url { get; set; } = string.Empty;
   public EngineType EngineType { get; set; }
   public List<TrackerHistory> Histories { get; set; } = [];
}
