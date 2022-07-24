using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrack.SEOTracker.Services.Helpers
{
   public static class FetchPageHelper
   {
      public async static Task<string> GetPage(string pageUrl)
      {

         StringBuilder sb = new();
         byte[] ResultsBuffer;
         var httpClient = new HttpClient();
         httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36");
         var stream = await httpClient.GetStreamAsync(pageUrl);
         using (MemoryStream ms = new MemoryStream())
         {
            stream.CopyTo(ms);
            ResultsBuffer = ms.ToArray();
         }

         var finalHtml = "";
         if (ResultsBuffer.Any())
         {
            sb.Append(Encoding.ASCII.GetString(ResultsBuffer, 0, ResultsBuffer.Length));
            finalHtml = sb.ToString();
         }
         return finalHtml;
      }
   }
}
