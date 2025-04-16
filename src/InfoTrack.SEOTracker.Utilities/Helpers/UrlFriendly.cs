using MongoDB.Bson;

namespace InfoTrack.SEOTracker.Utilities.Helpers;

public static class UrlFriendly
{
   /// <summary>
   /// Converts a string to a URL friendly string
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string ToUrlFromString(this string input) => Uri.EscapeDataString(input);

   /// <summary>
   /// Converts Url from GUID
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string ToUrlFromGuid(this Guid input) => Uri.EscapeDataString(Convert.ToBase64String(input.ToByteArray()));

   /// <summary>
   /// Converts Url from ObjectId
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string ToUrlFromObjectId(this ObjectId input) => Uri.EscapeDataString(Convert.ToBase64String(input.ToByteArray()));

   /// <summary>
   /// Converts Url from ObjectId
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string? ToUrlFromObjectIdNullable(this ObjectId? input)
       => input.HasValue ? Uri.EscapeDataString(Convert.ToBase64String(input.Value.ToByteArray())) : null;
   public static string? ToUrlFromObjectId(this ObjectId? input) => input == null ? null : Uri.EscapeDataString(Convert.ToBase64String(input.Value.ToByteArray()));

   /// <summary>
   /// Converts ObjectId from Url
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static ObjectId? ToObjectIdNullable(this string? input)
     => !string.IsNullOrEmpty(input) ? new ObjectId(Convert.FromBase64String(Uri.UnescapeDataString(input.ToString()))) : null;

   /// <summary>
   /// Converts ObjectId from Url
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static ObjectId ToObjectId(this string? input)
         => input.ToObjectIdNullable() ?? throw new ArgumentException("Invalid key.");


   /// <summary>
   /// Converts ObjectId from Url
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string ToObjectIdToString(this string? input)
      => input.ToObjectIdNullable().ToString() ?? throw new ArgumentException("Invalid key.");

   /// <summary>
   /// Converts ObjectId from Url
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string? ToObjectIdToStringNullable(this string? input)
      => input.ToObjectIdNullable()?.ToString() ?? null;


   /// <summary>
   /// Converts a string from URL friendly string
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string ToStringFromUrl(this string input)
     => string.IsNullOrEmpty(input) ? string.Empty : Uri.UnescapeDataString(input);

   /// <summary>
   /// Converts Url from GUID
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static Guid? ToGuidFromUrl(this string? input) => string.IsNullOrEmpty(input) ? null : new Guid(Convert.FromBase64String(Uri.UnescapeDataString(input.ToString())));
}
