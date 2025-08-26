using KopiBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KopiBudget.Infrastructure.Configuration
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Link)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(m => m.Permissions)
                   .WithOne(p => p.Module)
                   .HasForeignKey(p => p.ModuleId);

            builder.HasOne(p => p.CreatedBy)
                   .WithMany()
                   .HasForeignKey(p => p.CreatedById)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.UpdatedBy)
                   .WithMany()
                   .HasForeignKey(p => p.UpdatedById)
                   .OnDelete(DeleteBehavior.NoAction);
        }

        #endregion Public Methods
    }
}