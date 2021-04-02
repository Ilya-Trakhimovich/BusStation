using Lab06.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab06.DAL.DbContext.EntityConfiguration
{
    public class BusConfiguration : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasAlternateKey(p => p.RegisterNumber);
            builder.Property(p => p.SeatsCount).HasDefaultValue(20);
            builder.Property(p => p.RegisterNumber).IsRequired().HasMaxLength(7);
        }
    }
}
