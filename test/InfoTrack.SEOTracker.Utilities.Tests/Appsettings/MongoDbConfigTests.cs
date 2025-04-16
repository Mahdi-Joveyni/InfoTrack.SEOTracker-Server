using InfoTrack.SEOTracker.Utilities.Appsettings;
using InfoTrack.SEOTracker.Utilities.Helpers;
using System.Text;

namespace InfoTrack.SEOTracker.Utilities.Tests.Appsettings;
public class MongoDbConfigTests
{
   public MongoDbConfigTests()
   {
      EncryptionHelper.Key = "L/mOvgSkEwIx6cU00TaSRO6TbAoONVV+XN+Yr3/xXRk=";
   }
   [Fact]
   public void Password_ShouldReturnDecryptedPassword_WhenEncryptedPasswordIsValid()
   {
      // Arrange
      var config = new MongoDbConfig();
      config.EncryptedPassword = "HelloWorld!".EncryptObject();

      // Act
      var password = config.Password;

      // Assert
      Assert.Equal("HelloWorld!", password);
   }

   [Fact]
   public void ConnectionString_ShouldReturnTempConnection_WhenTempConnectionIsSet()
   {
      // Arrange
      var config = new MongoDbConfig
      {
         TempConnection = "mongodb://temp-connection"
      };

      // Act
      var connectionString = config.ConnectionString;

      // Assert
      Assert.Equal("mongodb://temp-connection", connectionString);
   }

   [Fact]
   public void ConnectionString_ShouldReturnConnectionStringWithoutAuthentication_WhenUserIsEmpty()
   {
      // Arrange
      var config = new MongoDbConfig
      {
         Server = "localhost",
         Port = "27017"
      };

      // Act
      var connectionString = config.ConnectionString;

      // Assert
      Assert.Equal("mongodb://localhost:27017", connectionString);
   }

   [Fact]
   public void ConnectionString_ShouldReturnConnectionStringWithAuthentication_WhenUserAndPasswordAreSet()
   {
      // Arrange
      var config = new MongoDbConfig
      {
         Server = "localhost",
         Port = "27017",
         User = "testUser"
      };
      EncryptionHelper.Key = "L/mOvgSkEwIx6cU00TaSRO6TbAoONVV+XN+Yr3/xXRk=";
      config.EncryptedPassword = "testPassword".Encrypt(EncryptionHelper.Key);

      // Act
      var connectionString = config.ConnectionString;

      // Assert
      Assert.Equal("mongodb://testUser:testPassword@localhost:27017", connectionString);
   }
}