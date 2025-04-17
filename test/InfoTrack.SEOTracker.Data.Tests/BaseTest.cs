using Mongo2Go;

namespace InfoTrack.SEOTracker.Data.Tests;
public abstract class BaseTest : IClassFixture<MongoDbFixture>, IDisposable
{
   protected readonly MongoDbRunner _runner;
   protected BaseTest(MongoDbFixture fixture)
   {
      fixture.Initialize();
      _runner = fixture.Runner!;
   }

   public void Dispose()
   {
      _runner.Dispose();
      GC.SuppressFinalize(this);
   }
}
