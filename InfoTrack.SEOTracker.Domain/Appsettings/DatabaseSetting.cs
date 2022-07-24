namespace InfoTrack.SEOTracker.Domain.Appsettings
{
   public class DatabaseSetting
   {
      public const string Position = "Database";
      public string Server { get; set; }
      public string Catalog { get; set; }
      public string User { get; set; }
      public string Password { get; set; }

      public string ConnectionStringPatternWindowsUser { get; set; }
      public string ConnectionStringPattern { get; set; }

      public string ConnectionString
      {
         get
         {
            if (string.IsNullOrEmpty(User))
            {
               return string.Format(ConnectionStringPatternWindowsUser, Server, Catalog);
            }
            else
               return string.Format(ConnectionStringPattern, Server, Catalog, User, Password);
         }
      }
   }
}
