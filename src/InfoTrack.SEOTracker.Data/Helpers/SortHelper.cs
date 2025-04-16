using MongoDB.Driver;
using System.ComponentModel;


namespace InfoTrack.SEOTracker.Data.Helpers;

public static class SortHelper
{
   public static IAggregateFluent<T> ApplySort<T>(this IAggregateFluent<T> query, string orderByQueryString, ListSortDirection sortOrder)
   {
      if (!query.Any())
         return query;

      if (string.IsNullOrWhiteSpace(orderByQueryString))
      {
         return query;
      }

      var orderParams = orderByQueryString.Trim().Split(',');
      Type type = typeof(T);
      SortDefinition<T>? sortDefinition = null;
      foreach (var propertyName in orderParams)
      {
         var propertyInfo = type.GetProperty(propertyName);
         if (propertyInfo is not null)
         {
            if (sortDefinition is null)
               sortDefinition = sortOrder == ListSortDirection.Ascending ? Builders<T>.Sort.Ascending(propertyName) : Builders<T>.Sort.Descending(propertyName);
            else
               sortDefinition = sortOrder == ListSortDirection.Ascending ? sortDefinition.Ascending(propertyName) : sortDefinition.Descending(propertyName);
         }
      }
      if (sortDefinition is null)
         return query;
      return query.Sort(sortDefinition);
   }
}
