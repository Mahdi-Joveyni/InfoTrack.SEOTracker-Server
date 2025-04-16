using Mongo2Go;

namespace InfoTrack.SEOTracker.Data.Tests;
public abstract class BaseTest : IClassFixture<MongoDbFixture>
{
   protected readonly MongoDbRunner _runner;
   protected BaseTest(MongoDbFixture fixture)
   {
      fixture.InitializeAsync().GetAwaiter().GetResult();
      _runner = fixture.Runner!;
   }
}
