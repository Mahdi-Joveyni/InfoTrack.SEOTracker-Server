using InfoTrack.SEOTracker.Api.Dependencies;
using InfoTrack.SEOTracker.Api.Middlewares;
using InfoTrack.SEOTracker.Services.Dependencies;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var appSetting = builder.Services.RegisterAppSettings(builder.Configuration);
// Add services to the container.
builder.Services.AddServiceDependencies(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(opt =>
{
   opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SEO Tracker", Version = "v1" });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorLoggingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();
app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
app.Run();

public partial class Program { }