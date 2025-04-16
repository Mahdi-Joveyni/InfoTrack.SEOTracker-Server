using InfoTrack.SEOTracker.Utilities.Helpers;

namespace InfoTrack.SEOTracker.Utilities.Tests.Helpers;
public class EncryptionHelperTests
{
   public EncryptionHelperTests()
   {
      EncryptionHelper.Key = "L/mOvgSkEwIx6cU00TaSRO6TbAoONVV+XN+Yr3/xXRk=";
   }
   [Fact]
   public void Encrypt_ShouldReturnEncryptedString()
   {
      // Arrange
      var plainText = "Hello World!";

      // Act
      var encryptedText = plainText.EncryptObject();

      // Act
      var decryptedText = encryptedText.DecryptObject();

      // Assert
      Assert.NotNull(decryptedText);
      Assert.Equal(plainText, decryptedText);

      // Assert
      Assert.NotNull(encryptedText);
      Assert.NotEqual(plainText, encryptedText);
   }

   [Fact]
   public void Decrypt_ShouldReturnOriginalString()
   {
      // Arrange
      var plainText = "Hello World!";
      var key = "L/mOvgSkEwIx6cU00TaSRO6TbAoONVV+XN+Yr3/xXRk="; // 16-byte key
      var encryptedText = plainText.Encrypt(key);

      // Act
      var decryptedText = encryptedText.Decrypt(key);

      // Assert
      Assert.NotNull(decryptedText);
      Assert.Equal(plainText, decryptedText);
   }

   [Fact]
   public void EncryptObject_ShouldThrowException_WhenKeyIsEmpty()
   {
      // Arrange
      EncryptionHelper.Key = string.Empty;
      var plainText = "Hello World!";

      // Act & Assert
      Assert.Throws<ArgumentNullException>(() => plainText.EncryptObject());
   }

   [Fact]
   public void DecryptObject_ShouldReturnNull_WhenDecryptionFails()
   {
      // Arrange
      var invalidEncryptedText = "InvalidEncryptedText";

      // Act
      var result = invalidEncryptedText.DecryptObject();

      // Assert
      Assert.Null(result);
   }
}