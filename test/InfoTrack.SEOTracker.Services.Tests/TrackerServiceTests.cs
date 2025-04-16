using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Domain.DTO;
using InfoTrack.SEOTracker.Domain.Enumerations;
using InfoTrack.SEOTracker.Utilities.Helpers;
using MongoDB.Bson;
using Moq;
using System.Linq.Expressions;

namespace InfoTrack.SEOTracker.Services.Tests;
public class TrackerServiceTests
{
   [Fact]
   public async Task GetAllTracker_ShouldReturnDataTableResultWithMappedItems()
   {
      // Arrange
      var mockRepository = new Mock<IMongoRepository<Tracker>>();
      var dataTableRequest = new DataTableRequest
      {
         PageNumber = 1,
         PageSize = 10
      };

      var trackerItems = new List<Tracker>
        {
            new () { Id =ObjectId.GenerateNewId(), Search = "Test Search 1", Url = "http://example1.com", EngineType = EngineType.Google, Histories = [new TrackerHistory() { Ranks = [1,2] }] },
            new () { Id =ObjectId.GenerateNewId(), Search = "Test Search 2", Url = "http://example2.com", EngineType = EngineType.Google,Histories = [new TrackerHistory() { Ranks = [1,2] }]  }
        };

      var trackerResult = new DataTableResult<Tracker>
      {
         Items = trackerItems,
         TotalCount = trackerItems.Count
      };

      mockRepository
          .Setup(repo => repo.FilterByAsync(It.IsAny<Expression<Func<Tracker, bool>>>(),
          dataTableRequest, It.IsAny<CancellationToken>()))
          .ReturnsAsync(trackerResult);

      var service = new TrackerService(mockRepository.Object);

      // Act
      var result = await service.GetAllTracker(dataTableRequest, TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(trackerItems.Count, result.TotalCount);
      Assert.Equal(trackerItems.Count, result.Items.Count);
      Assert.Equal(trackerItems[0].Id.ToUrlFromObjectId(), result.Items[0].Key);
      Assert.Equal("Test Search 1", result.Items[0].Search);
      Assert.Equal("http://example1.com", result.Items[0].Url);
      Assert.Equal(EngineType.Google, result.Items[0].EngineType);
   }

   [Fact]
   public async Task GetByKey_ShouldReturnTrackerDto_WhenTrackerExists()
   {
      // Arrange
      var mockRepository = new Mock<IMongoRepository<Tracker>>();
      var trackerId = ObjectId.GenerateNewId();
      var tracker = new Tracker
      {
         Id = trackerId,
         Search = "Test Search",
         Url = "http://example.com",
         EngineType = Domain.Enumerations.EngineType.Google,
         Histories = new List<TrackerHistory>
            {
                new TrackerHistory { CreateDateTime = DateTime.UtcNow.AddDays(-1), Ranks = new List<int> { 1, 2 } },
                new TrackerHistory { CreateDateTime = DateTime.UtcNow, Ranks = new List<int> { 3, 4 } }
            }
      };

      mockRepository
          .Setup(repo => repo.FindByIdAsync(trackerId, It.IsAny<CancellationToken>()))
          .ReturnsAsync(tracker);

      var service = new TrackerService(mockRepository.Object);

      // Act
      var result = await service.GetByKey(trackerId.ToUrlFromObjectId(), CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(trackerId.ToUrlFromObjectId(), result.Key);
      Assert.Equal("Test Search", result.Search);
      Assert.Equal("http://example.com", result.Url);
      Assert.Equal(Domain.Enumerations.EngineType.Google, result.EngineType);
      Assert.Equal(2, result.Histories.Count);
      Assert.Equal(3, result.Histories.First().Ranks.First()); // Most recent history should be first
   }

   [Fact]
   public async Task GetByKey_ShouldReturnNull_WhenTrackerDoesNotExist()
   {
      // Arrange
      var mockRepository = new Mock<IMongoRepository<Tracker>>();
      var trackerId = ObjectId.GenerateNewId();

      mockRepository
          .Setup(repo => repo.FindByIdAsync(trackerId, It.IsAny<CancellationToken>()))
          .ReturnsAsync((Tracker?)null);

      var service = new TrackerService(mockRepository.Object);

      // Act
      var result = await service.GetByKey(trackerId.ToUrlFromObjectId(), CancellationToken.None);

      // Assert
      Assert.Null(result);
   }
}