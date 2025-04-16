using System.ComponentModel;

namespace InfoTrack.SEOTracker.Domain;

public class DataTableRequest
{
   private int pageSize = 0;

   public int PageSize
   {
      get
      {

         return pageSize;
      }
      set
      {
         if (value < 0)
         {
            pageSize = 0;
            return;
         }
         pageSize = value;
      }
   }

   private int pageNumber = 1;
   public int PageNumber
   {
      get
      {
         return pageNumber;
      }
      set
      {
         if (value < 1)
         {
            pageNumber = 1;
            return;
         }
         pageNumber = value;
      }
   }
   public string? SearchTerm { get; set; }
   public string? Sort { get; set; } = "Id";
   public ListSortDirection SortType { get; set; } = ListSortDirection.Ascending;

   public Dictionary<string, string?> ToDictionary()
   {
      var dic = new Dictionary<string, string?>()
            {
                { nameof(PageNumber) , PageNumber.ToString() },
                { nameof(PageSize), PageSize.ToString() }
            };

      if (!string.IsNullOrEmpty(SearchTerm))
         dic.Add(nameof(SearchTerm), SearchTerm);

      if (!string.IsNullOrEmpty(Sort))
      {
         dic.Add(nameof(Sort), Sort);
         dic.Add(nameof(SortType), SortType.ToString());
      }

      return dic;
   }
}