using System.Security.Cryptography;
using System.Text;

namespace InfoTrack.SEOTracker.Utilities.Helpers;
public static class EncryptionHelper
{
   public static string Key { get; set; } = string.Empty;

   private static readonly string IV = "TlQy3eDvn6tOMnipOo2yHg==";

   public static string EncryptObject(this string plainText) => !string.IsNullOrEmpty(Key) ? plainText.Encrypt(Key) : throw new ArgumentNullException("invalid key");
   public static string? DecryptObject(this string plainText) => !string.IsNullOrEmpty(Key) ? plainText.Decrypt(Key) : throw new ArgumentNullException("invalid key");
   public static string Encrypt(this string plainText, string key)
   {
      using var aes = Aes.Create();
      aes.Key = Convert.FromBase64String(key);
      aes.IV = Convert.FromBase64String(IV);
      var encryptor = aes.CreateEncryptor();

      byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
      byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

      return Convert.ToBase64String(encryptedBytes);
   }

   public static string? Decrypt(this string encryptedText, string key)
   {
      try
      {
         byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

         using var aes = Aes.Create();
         aes.Key = Convert.FromBase64String(key);
         aes.IV = Convert.FromBase64String(IV);
         var decryptor = aes.CreateDecryptor();

         byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
         return Encoding.UTF8.GetString(decryptedBytes);
      }
      catch
      {
         return null;
      }
   }
}