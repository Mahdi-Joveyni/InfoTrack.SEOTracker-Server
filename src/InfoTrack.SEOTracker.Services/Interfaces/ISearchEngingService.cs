namespace InfoTrack.SEOTracker.Services.Interfaces;

public interface ISearchEngingService
{
   Task<List<int>> GetRank(string search, string url, CancellationToken cancellationToken = default);
}