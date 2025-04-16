namespace InfoTrack.SEOTracker.Domain.DTO;

public class TrackerHistory
{
   public List<int> Ranks { get; set; } = [];
   public DateTime CreateDateTime { get; set; } = DateTime.Now;
}
