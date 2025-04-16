using InfoTrack.SEOTracker.Api.Tests;
using InfoTrack.SEOTracker.Api.Tests.ClientServices;

namespace InfoTrack.SEOTracker.Data.Tests.ControllerTest;

public class TrackerClientServiceTest : ControllerTestBase<Program>, IClassFixture<CustomWebApplicationFactory<Program>>
{
   private readonly TrackerClientService _trackerClientService;

   public TrackerClientServiceTest(CustomWebApplicationFactory<Program> webApplicationFactory) : base(webApplicationFactory)
   {
      _trackerClientService = new TrackerClientService(LocalHttpClient);
   }

   [Fact]
   public async Task TrackerClientService_HappyScenario()
   {
      // GetAll
      var getAllResult = await _trackerClientService.GetAllTracker(new Domain.DataTableRequest(), TestContext.Current.CancellationToken);
      Assert.Empty(getAllResult.Items);

      // Create
      var createResult = await _trackerClientService.GetGoogleRank("land registry search",
         "infotrack.co.uk", TestContext.Current.CancellationToken);
      Assert.NotEmpty(createResult);

      getAllResult = await _trackerClientService.GetAllTracker(new Domain.DataTableRequest(), TestContext.Current.CancellationToken);
      Assert.NotEmpty(getAllResult.Items);

      var getByKeyResult = await _trackerClientService.GetByKey(getAllResult.Items[0].Key, TestContext.Current.CancellationToken);
      Assert.NotNull(getByKeyResult);

      // Create
      createResult = await _trackerClientService.GetGoogleRank("land registry",
         "infotrack.co.uk", TestContext.Current.CancellationToken);
      Assert.NotEmpty(createResult);

      getAllResult = await _trackerClientService.GetAllTracker(new Domain.DataTableRequest(), TestContext.Current.CancellationToken);
      Assert.Equal(2, getAllResult.TotalCount);

   }
}