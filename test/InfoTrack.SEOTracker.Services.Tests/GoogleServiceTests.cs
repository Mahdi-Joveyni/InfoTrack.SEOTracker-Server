using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Services.Interfaces;
using InfoTrack.SEOTracker.Utilities.Appsettings;
using Microsoft.Extensions.Options;
using Moq;

namespace InfoTrack.SEOTracker.Services.Tests;

public class GoogleServiceTests
{
   private readonly GoogleService _service;
   private readonly Mock<IHtmlRenderService> _htmlRenderService;
   private readonly Mock<IMongoRepository<Tracker>> _mockRepository;

   public GoogleServiceTests()
   {
      _htmlRenderService = new Mock<IHtmlRenderService>();

      // Setup AppSetting
      var appSetting = Options.Create(new AppSetting { PageSize = 10 });

      // Setup Mock Repository
      _mockRepository = new Mock<IMongoRepository<Tracker>>();

      // Initialize GoogleService
      _service = new GoogleService(_htmlRenderService.Object, appSetting, _mockRepository.Object);
   }

   //[Fact]
   //public async Task GetRank_ShouldReturnCorrectRanks()
   //{
   //   // Arrange
   //   _htmlRenderService.Setup(x => x.GetHtmlContentAsync(It.Is<string>(x => x.StartsWith("https://www.google.com"))
   //      , It.IsAny<CancellationToken>())).ReturnsAsync("<a href=\"http://example.com\"><h3>Example</h3></a>");

   //   // Act
   //   var ranks = await _service.GetRank("test search", "example.com");

   //   // Assert
   //   Assert.NotNull(ranks);
   //   Assert.Contains(1, ranks); // Rank 1 because the URL matches in the mocked HTML
   //}

   //[Fact]
   //public async Task GetRank_ShouldInsertNewTracker_WhenTrackerDoesNotExist()
   //{
   //   // Arrange
   //   _htmlRenderService.Setup(x => x.GetHtmlContentAsync(It.Is<string>(x => x.StartsWith("https://www.google.com"))
   //      , It.IsAny<CancellationToken>())).ReturnsAsync("<a href=\"http://example.com\"><h3>Example</h3></a>");

   //   _mockRepository
   //       .Setup(repo => repo.FindOneAsync(It.IsAny<Expression<Func<Tracker, bool>>>(), It.IsAny<CancellationToken>()))
   //       .ReturnsAsync((Tracker?)null);

   //   _mockRepository
   //       .Setup(repo => repo.InsertOneAsync(It.IsAny<Tracker>(), It.IsAny<CancellationToken>()))
   //       .ReturnsAsync(new Tracker { Id = ObjectId.GenerateNewId() });

   //   // Act
   //   var ranks = await _service.GetRank("test search", "example.com");

   //   // Assert
   //   Assert.NotNull(ranks);
   //   Assert.Contains(1, ranks); // Rank 1 because the URL matches in the mocked HTML
   //   _mockRepository.Verify(repo => repo.InsertOneAsync(It.IsAny<Tracker>(), It.IsAny<CancellationToken>()), Times.Once);
   //}
}