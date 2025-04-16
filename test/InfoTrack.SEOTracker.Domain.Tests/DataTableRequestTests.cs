using System.ComponentModel;

namespace InfoTrack.SEOTracker.Domain.Tests;
public class DataTableRequestTests
{
   [Fact]
   public void PageSize_ShouldDefaultToZero_WhenNotSet()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      var pageSize = request.PageSize;

      // Assert
      Assert.Equal(0, pageSize);
   }

   [Fact]
   public void PageSize_ShouldNotAllowNegativeValues()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      request.PageSize = -5;

      // Assert
      Assert.Equal(0, request.PageSize);
   }

   [Fact]
   public void PageNumber_ShouldDefaultToOne_WhenNotSet()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      var pageNumber = request.PageNumber;

      // Assert
      Assert.Equal(1, pageNumber);
   }

   [Fact]
   public void PageNumber_ShouldNotAllowValuesLessThanOne()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      request.PageNumber = 0;

      // Assert
      Assert.Equal(1, request.PageNumber);
   }

   [Fact]
   public void SearchTerm_ShouldBeNullByDefault()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      var searchTerm = request.SearchTerm;

      // Assert
      Assert.Null(searchTerm);
   }

   [Fact]
   public void Sort_ShouldDefaultToId_WhenNotSet()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      var sort = request.Sort;

      // Assert
      Assert.Equal("Id", sort);
   }

   [Fact]
   public void SortType_ShouldDefaultToAscending_WhenNotSet()
   {
      // Arrange
      var request = new DataTableRequest();

      // Act
      var sortType = request.SortType;

      // Assert
      Assert.Equal(ListSortDirection.Ascending, sortType);
   }

   [Fact]
   public void ToDictionary_ShouldIncludeAllProperties()
   {
      // Arrange
      var request = new DataTableRequest
      {
         PageNumber = 2,
         PageSize = 10,
         SearchTerm = "test",
         Sort = "Name",
         SortType = ListSortDirection.Descending
      };

      // Act
      var dictionary = request.ToDictionary();

      // Assert
      Assert.Equal("2", dictionary["PageNumber"]);
      Assert.Equal("10", dictionary["PageSize"]);
      Assert.Equal("test", dictionary["SearchTerm"]);
      Assert.Equal("Name", dictionary["Sort"]);
      Assert.Equal("Descending", dictionary["SortType"]);
   }

   [Fact]
   public void ToDictionary_ShouldExcludeNullOrEmptyProperties()
   {
      // Arrange
      var request = new DataTableRequest
      {
         PageNumber = 1,
         PageSize = 5,
         SearchTerm = null,
         Sort = null
      };

      // Act
      var dictionary = request.ToDictionary();

      // Assert
      Assert.Equal("1", dictionary["PageNumber"]);
      Assert.Equal("5", dictionary["PageSize"]);
      Assert.False(dictionary.ContainsKey("SearchTerm"));
      Assert.False(dictionary.ContainsKey("Sort"));
      Assert.False(dictionary.ContainsKey("SortType"));
   }
}