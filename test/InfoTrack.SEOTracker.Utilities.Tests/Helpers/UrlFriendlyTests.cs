using InfoTrack.SEOTracker.Utilities.Helpers;
using MongoDB.Bson;

namespace InfoTrack.SEOTracker.Utilities.Tests.Helpers;

public class UrlFriendlyTests
{
   [Fact]
   public void ToUrlFromString_ShouldReturnUrlEncodedString()
   {
      // Arrange
      var input = "Hello World!";

      // Act
      var result = UrlFriendly.ToUrlFromString(input);

      // Assert
      Assert.Equal("Hello%20World%21", result);
   }

   [Fact]
   public void ToUrlFromGuid_ShouldReturnUrlEncodedGuid()
   {
      // Arrange
      var guid = Guid.NewGuid();

      // Act
      var result = guid.ToUrlFromGuid();

      // Assert
      var decodedGuid = new Guid(Convert.FromBase64String(Uri.UnescapeDataString(result)));
      Assert.Equal(guid, decodedGuid);
   }

   [Fact]
   public void ToUrlFromObjectId_ShouldReturnUrlEncodedObjectId()
   {
      // Arrange
      var objectId = ObjectId.GenerateNewId();

      // Act
      var result = objectId.ToUrlFromObjectId();

      // Assert
      var decodedObjectId = new ObjectId(Convert.FromBase64String(Uri.UnescapeDataString(result)));
      Assert.Equal(objectId, decodedObjectId);
   }

   [Fact]
   public void ToObjectIdFromUrl_ShouldReturnObjectId()
   {
      // Arrange
      var objectId = ObjectId.GenerateNewId();
      var urlEncodedObjectId = objectId.ToUrlFromObjectId();

      // Act
      var result = urlEncodedObjectId.ToObjectId();

      // Assert
      Assert.Equal(objectId, result);
   }

   [Fact]
   public void ToStringFromUrl_ShouldReturnDecodedString()
   {
      // Arrange
      var input = "Hello%20World%21";

      // Act
      var result = UrlFriendly.ToStringFromUrl(input);

      // Assert
      Assert.Equal("Hello World!", result);
   }

   [Fact]
   public void ToGuidFromUrl_ShouldReturnGuid()
   {
      // Arrange
      var guid = Guid.NewGuid();
      var urlEncodedGuid = guid.ToUrlFromGuid();

      // Act
      var result = urlEncodedGuid.ToGuidFromUrl();

      // Assert
      Assert.Equal(guid, result);
   }

   [Fact]
   public void ToObjectIdFromUrl_ShouldThrowArgumentException_WhenInputIsNullOrEmpty()
   {
      // Arrange
      string? input = null;

      // Act & Assert
      Assert.Throws<ArgumentException>(() => input.ToObjectId());
   }

   [Fact]
   public void ToGuidFromUrl_ShouldThrowArgumentException_WhenInputIsNullOrEmpty()
   {
      // Arrange
      string? input = null;

      // Act & Assert
      Assert.Null(input.ToGuidFromUrl());
   }

   [Fact]
   public void ToObjectIdToStringNullable_NullOrEmptyInput_ShouldReturnNull()
   {
      // Arrange
      string? input = null;

      // Act
      var result = input.ToObjectIdToStringNullable();

      // Assert
      Assert.Null(result);
   }

   [Fact]
   public void ToUrlFromObjectId_NullInput_ShouldReturnNull()
   {
      // Arrange
      ObjectId? objectId = null;

      // Act
      var result = objectId.ToUrlFromObjectId();

      // Assert
      Assert.Null(result);
   }

}