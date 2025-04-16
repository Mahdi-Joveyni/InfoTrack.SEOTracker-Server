using Mongo2Go;

namespace InfoTrack.SEOTracker.Data.Tests;
public class MongoDbFixture : IAsyncLifetime
{
   public MongoDbRunner? Runner { get; private set; }

   public ValueTask InitializeAsync()
   {
      Runner?.Dispose();
      Runner = MongoDbRunner.Start();
      Task.Delay(500).Wait();
      return ValueTask.CompletedTask;
   }


   public ValueTask DisposeAsync()
   {
      Runner?.Dispose();
      Runner = null;
      GC.SuppressFinalize(this);
      return ValueTask.CompletedTask;
   }
}
