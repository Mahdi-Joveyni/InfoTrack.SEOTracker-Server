namespace InfoTrack.SEOTracker.Domain;

public class DataTableResult<T> where T : class
{
   public List<T> Items { get; set; } = [];
   public int TotalCount { get; set; }
}
