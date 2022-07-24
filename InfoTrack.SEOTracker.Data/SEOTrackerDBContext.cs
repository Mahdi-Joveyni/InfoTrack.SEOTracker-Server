using InfoTrack.SEOTracker.Data.Configurations;
using InfoTrack.SEOTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InfoTrack.SEOTracker.Data
{
   public class SEOTrackerDBContext : DbContext
   {
      public DbSet<Tracker> Trackers { get; set; }

      public SEOTrackerDBContext(DbContextOptions<SEOTrackerDBContext> options)
          : base(options)
      {

      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfigurationsFromAssembly(
         Assembly.GetAssembly(typeof(TrackersConfiguration)),
         x => x.Namespace!.Equals(typeof(TrackersConfiguration).Namespace));
      }
   }
}
