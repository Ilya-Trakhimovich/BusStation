using Lab06.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab06.DAL.DbContext.EntityConfiguration
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Cost).IsRequired().HasDefaultValue(10);
            builder.Property(p => p.StartDate).IsRequired().HasColumnType("date");
            builder.Property(p => p.FinishDate).IsRequired().HasColumnType("date");
            builder.Property(p => p.StartTrip).IsRequired().HasColumnType("time");
            builder.Property(p => p.FinishTip).IsRequired().HasColumnType("time");
            builder.Property(p => p.CountSoldSeats).IsRequired();
            builder.Property(p => p.FreeSeats).IsRequired();
            builder.Property(p => p.IsCanceled).HasDefaultValue(false);
            builder.HasOne(p => p.Bus).WithMany(p => p.Trips).HasForeignKey(f => f.BusId);
            builder.HasOne(p => p.City).WithMany(p => p.Trips).HasForeignKey(f => f.CityId);
        }
    }
}