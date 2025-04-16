namespace InfoTrack.SEOTracker.Data.Models.Base;

public class LookupOptions
{
   public required string From { get; set; }
   public required string LocalField { get; set; }
   public required string ForeignField { get; set; }
   public required string AsField { get; set; }
}

