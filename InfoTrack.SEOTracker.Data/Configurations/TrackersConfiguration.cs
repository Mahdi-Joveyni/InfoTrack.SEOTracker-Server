using InfoTrack.SEOTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTrack.SEOTracker.Data.Configurations
{
   internal class TrackersConfiguration : IEntityTypeConfiguration<Tracker>
   {
      public void Configure(EntityTypeBuilder<Tracker> builder)
      {
         builder.HasKey(e => e.Id);

         builder.Property(e => e.Search)
            .IsRequired();

         builder.Property(e => e.Url);
      }
   }
}
