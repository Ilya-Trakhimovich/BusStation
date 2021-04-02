using Lab06.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab06.DAL.DbContext.EntityConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {           
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(10);
            builder.Property(p => p.PasswordHash).IsRequired();            
        }
    }
}