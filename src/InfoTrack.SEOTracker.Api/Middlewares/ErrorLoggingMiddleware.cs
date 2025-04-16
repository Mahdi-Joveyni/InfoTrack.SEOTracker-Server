using Serilog;
using System.Text.Json;

namespace InfoTrack.SEOTracker.Api.Middlewares;

public class ErrorLoggingMiddleware(RequestDelegate next)
{
   public async Task Invoke(HttpContext context)
   {
      try
      {
         await next(context);
      }
      catch (Exception ex)
      {
         Log.Logger.Error(ex, ex.Message);
         context.Response.StatusCode = 500;
         await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Error"));
      }
   }
}