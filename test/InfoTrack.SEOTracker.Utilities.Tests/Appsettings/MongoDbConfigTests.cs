using InfoTrack.SEOTracker.Utilities.Appsettings;
using InfoTrack.SEOTracker.Utilities.Helpers;
using System.Text;

namespace InfoTrack.SEOTracker.Utilities.Tests.Appsettings;
public class MongoDbConfigTests
{
   [Fact]
   public void Password_ShouldReturnDecryptedPassword_WhenEncryptedPasswordIsValid()
   {
      // Arrange
      var config = new MongoDbConfig();
      EncryptionHelper.Key = "L/mOvgSkEwIx6cU00TaSRO6TbAoONVV+XN+Yr3/xXRk=";
      config.EncryptedPassword = "HelloWorld!".Encrypt(EncryptionHelper.Key);

      // Act
      var password = config.Password;

      // Assert
      Assert.Equal("HelloWorld!", password);
   }

   [Fact]
   public void Password_ShouldReturnEncryptedPassword_WhenDecryptionFails()
   {
      // Arrange
      var plainPassword = "plain password";
      var config = new MongoDbConfig
      {
         EncryptedPassword = plainPassword
      };

      // Act
      var password = config.Password;

      // Assert
      Assert.Equal(plainPassword, password);
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
      EncryptionHelper.Key = Convert.ToBase64String(Encoding.UTF8.GetBytes("1234567890123456")); // 16-byte key
      config.EncryptedPassword = "testPassword".Encrypt(EncryptionHelper.Key);

      // Act
      var connectionString = config.ConnectionString;

      // Assert
      Assert.Equal("mongodb://testUser:testPassword@localhost:27017", connectionString);
   }
}