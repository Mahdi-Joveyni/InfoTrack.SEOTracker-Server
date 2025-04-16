using InfoTrack.SEOTracker.Data.Interfaces;
using InfoTrack.SEOTracker.Data.Models;
using InfoTrack.SEOTracker.Data.Models.Base;
using InfoTrack.SEOTracker.Data.Repositories;
using InfoTrack.SEOTracker.Domain;
using InfoTrack.SEOTracker.Utilities.Appsettings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.Security.Claims;

namespace InfoTrack.SEOTracker.Data.Tests;
public class MongoRepositoryTests : BaseTest
{
   private readonly IMongoRepository<Tracker> _repository;


   public MongoRepositoryTests(MongoDbFixture fixture) : base(fixture)
   {
      IOptions<MongoDbConfig> options = Options.Create(new MongoDbConfig
      {
         TempConnection = _runner.ConnectionString,
         DatabaseName = "ShareDataBase"
      });

      _repository = new MongoRepository<Tracker>(options);
   }

   [Fact]
   public async Task InsertOneAsync_ShouldInsertDocument()
   {
      // Arrange
      var document = new Tracker { Search = "Test Document" };

      // Act
      var result = await _repository.InsertOneAsync(document, TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("Test Document", result.Search);
   }

   [Fact]
   public async Task FindByIdAsync_ShouldReturnDocument_WhenDocumentExists()
   {
      // Arrange
      var document = new Tracker { Search = "Test Document" };
      var insertedDocument = await _repository.InsertOneAsync(document, TestContext.Current.CancellationToken);

      // Act
      var result = await _repository.FindByIdAsync(insertedDocument.Id, TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(insertedDocument.Id, result.Id);
      Assert.Equal("Test Document", result.Search);
   }

   [Fact]
   public async Task DeleteByIdAsync_ShouldDeleteDocument_WhenDocumentExists()
   {
      // Arrange
      var document = new Tracker { Search = "Test Document" };
      var insertedDocument = await _repository.InsertOneAsync(document, TestContext.Current.CancellationToken);

      // Act
      await _repository.DeleteByIdAsync(insertedDocument.Id, TestContext.Current.CancellationToken);
      var result = await _repository.FindByIdAsync(insertedDocument.Id, TestContext.Current.CancellationToken);

      // Assert
      Assert.Null(result);
   }

   [Fact]
   public async Task FilterByAsync_ShouldReturnDocuments_WhenDocumentsExist()
   {
      // Arrange
      var document1 = new Tracker { Search = "Test Document 1" };
      var document2 = new Tracker { Search = "Test Document 2" };
      await _repository.InsertOneAsync(document1, TestContext.Current.CancellationToken);
      await _repository.InsertOneAsync(document2, TestContext.Current.CancellationToken);

      // Act
      var result = await _repository.FilterByAsync(d => d.Search.Contains("Test Document"), TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, result.Count());
   }

   [Fact]
   public async Task InsertManyAsync_ShouldInsertDocuments()
   {
      // Arrange
      var documents = new List<Tracker>
        {
            new Tracker { Search = "Test Document 1" },
            new Tracker { Search = "Test Document 2" }
        };

      // Act
      await _repository.InsertManyAsync(documents, TestContext.Current.CancellationToken);
      var result = await _repository.FilterByAsync(d => d.Search.Contains("Test Document"), TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, result.Count());
   }

   [Fact]
   public async Task ReplaceOneAsync_ShouldReplaceDocument()
   {
      // Arrange
      var document = new Tracker { Search = "Test Document" };
      var insertedDocument = await _repository.InsertOneAsync(document, TestContext.Current.CancellationToken);
      insertedDocument.Search = "Updated Document";

      // Act
      var result = await _repository.ReplaceOneAsync(insertedDocument, TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("Updated Document", result.Search);
   }


   [Fact]
   public async Task DeleteOneAsync_ShouldDeleteDocument()
   {
      // Arrange
      var document = new Tracker { Search = "Test Document" };
      await _repository.InsertOneAsync(document, TestContext.Current.CancellationToken);

      // Act
      await _repository.DeleteOneAsync(d => d.Search == "Test Document", TestContext.Current.CancellationToken);
      var result = await _repository.FindOneAsync(d => d.Search == "Test Document", TestContext.Current.CancellationToken);

      // Assert
      Assert.Null(result);
   }

   [Fact]
   public async Task DeleteManyAsync_ShouldDeleteDocuments()
   {
      // Arrange
      var documents = new List<Tracker>
        {
            new Tracker { Search = "Test Document 1" },
            new Tracker { Search = "Test Document 2" }
        };
      await _repository.InsertManyAsync(documents, TestContext.Current.CancellationToken);

      // Act
      await _repository.DeleteManyAsync(d => d.Search.Contains("Test Document"), TestContext.Current.CancellationToken);
      var result = await _repository.FilterByAsync(d => d.Search.Contains("Test Document"), TestContext.Current.CancellationToken);

      // Assert
      Assert.Empty(result);
   }

   [Fact]
   public async Task FilterByAsync_WithParameters_ShouldReturnPagedDocuments()
   {
      // Arrange
      var documents = new List<Tracker>
        {
            new Tracker { Search = "Test Document 1" },
            new Tracker { Search = "Test Document 2" },
            new Tracker { Search = "Test Document 3" }
        };
      await _repository.InsertManyAsync(documents, TestContext.Current.CancellationToken);

      var parameters = new DataTableRequest
      {
         PageSize = 2,
         PageNumber = 0,
         Sort = "Search",
         SortType = ListSortDirection.Ascending
      };

      // Act
      var result = await _repository.FilterByAsync(d => d.Search.Contains("Test Document"), parameters, TestContext.Current.CancellationToken);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, result.Items.Count);
      Assert.Equal(3, result.TotalCount);
   }

   [Fact]
   public async Task FilterByAsync_WithLookupOptions_ShouldReturnJoinedDocuments()
   {
      // Arrange
      var document1 = new Tracker { Search = "Test Document 1" };
      var document2 = new Tracker { Search = "Test Document 2" };
      await _repository.InsertOneAsync(document1, TestContext.Current.CancellationToken);
      await _repository.InsertOneAsync(document2, TestContext.Current.CancellationToken);

      var parameters = new DataTableRequest
      {
         PageSize = 2,
         PageNumber = 1,
         Sort = "Search",
         SortType = ListSortDirection.Ascending
      };

      var lookupOptions = new LookupOptions
      {
         From = "OtherCollection",
         LocalField = "LocalField",
         ForeignField = "ForeignField",
         AsField = "JoinedField"
      };

      // Act
      var result = await _repository.FilterByAsync(d => d.Search.Contains("Test Document"), parameters, cancellationToken: TestContext.Current.CancellationToken, new LookupOptions[] { lookupOptions });

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, result.Items.Count);
   }

   [Fact]
   public async Task CountAsync_Functionality()
   {
      // Arrange
      var document1 = new Tracker { Search = "Test Document 1" };
      var document2 = new Tracker { Search = "Test Document 2" };
      await _repository.InsertOneAsync(document1, TestContext.Current.CancellationToken);
      await _repository.InsertOneAsync(document2, TestContext.Current.CancellationToken);

      var parameters = new DataTableRequest
      {
         PageSize = 2,
         PageNumber = 1,
         Sort = "Search",
         SortType = ListSortDirection.Ascending
      };


      // Act
      var result = await _repository.CountDocumentsAsync(d => d.Search.Contains("Test Document"), cancellationToken: TestContext.Current.CancellationToken);
      var result2 = await _repository.CountDocumentsAsync(d => d.Search == "", cancellationToken: TestContext.Current.CancellationToken);

      // Assert
      Assert.Equal(2, result);
      Assert.Equal(0, result2);
   }
}
