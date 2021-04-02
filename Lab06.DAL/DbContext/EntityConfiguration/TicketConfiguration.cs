using Lab06.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab06.DAL.DbContext.EntityConfiguration
{
    class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IsCanceled).HasDefaultValue(false);
            builder.HasOne(p => p.ApplicationUser).WithMany(p => p.Tickets).HasForeignKey(f => f.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Trip).WithMany(p => p.Tickets).HasForeignKey(f => f.TripId);
        }
    }
}
