using InfoTrack.SEOTracker.Utilities.Helpers;
using System.Text.Json.Serialization;

namespace InfoTrack.SEOTracker.Utilities.Appsettings;

public class MongoDbConfig
{
   public const string SectionName = "MongoDbConfig";
   public string Server { get; set; } = string.Empty;
   public string Port { get; set; } = string.Empty;
   public string DatabaseName { get; set; } = string.Empty;

   [JsonIgnore]
   public string User { get; set; } = string.Empty;
   [JsonIgnore]
   public string? TempConnection { get; set; }

   [JsonIgnore]
   public string EncryptedPassword { get; set; } = string.Empty;


   public string Password { get { return EncryptedPassword.DecryptObject() ?? EncryptedPassword; } }

   const string ConnectionStringPattern = "mongodb://{0}:{1}@{2}:{3}";
   const string ConnectionStringPatternWithoutAuthentication = "mongodb://{0}:{1}";

   [JsonIgnore]
   public string ConnectionString
   {
      get
      {
         if (!string.IsNullOrEmpty(TempConnection))
            return TempConnection;

         if (string.IsNullOrEmpty(User))
         {
            return string.Format(ConnectionStringPatternWithoutAuthentication, Server, Port);
         }
         else
         {
            return string.Format(ConnectionStringPattern, User, Password, Server, Port);
         }
      }
   }
}
