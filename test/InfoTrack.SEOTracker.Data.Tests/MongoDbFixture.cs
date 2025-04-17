using Mongo2Go;

namespace InfoTrack.SEOTracker.Data.Tests;
public class MongoDbFixture : IDisposable
{
   public MongoDbRunner? Runner { get; private set; }

   public void Initialize()
   {
      Runner?.Dispose();
      Runner = MongoDbRunner.Start();
   }

   public void Dispose()
   {
      Runner?.Dispose();
      Runner = null;
      GC.SuppressFinalize(this);
   }
}
