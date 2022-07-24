using InfoTrack.SEOTracker.Api.Dependencies;
using InfoTrack.SEOTracker.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var logger = builder.Services.RegisterLogger(builder.Configuration);
var appSetting = builder.Services.RegisterAppSettings(builder.Configuration);
// Add services to the container.
builder.Services.RegisterHelper();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.RegisterServices();
builder.Services.RegisterDbContext(builder.Configuration);


builder.Services.AddOpenApiDocument(document =>
{
   document.Title = appSetting.AppName;
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Error");
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();


}
else
{
   logger.Information($"Setup swagger.");
   app.UseOpenApi(); // serve OpenAPI/Swagger documents
   app.UseSwaggerUi3(); // serve Swagger UI
   app.UseReDoc(); // serve ReDoc UI
}
using (var scope = app.Services.CreateScope())
{
   var db = scope.ServiceProvider.GetRequiredService<SEOTrackerDBContext>();
   logger.Information($"{appSetting.AppName}'s DataBase Migrating.");
   try
   {
      db.Database.Migrate();
   }
   catch (Exception ex)
   {
      logger.Fatal(ex, $"{appSetting.AppName}'s DataBase Migration Failed");
   }
}

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());


app.MapControllers();

app.Run();
