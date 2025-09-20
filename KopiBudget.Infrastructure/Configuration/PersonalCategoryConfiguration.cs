using KopiBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KopiBudget.Infrastructure.Configuration
{
    public class PersonalCategoryConfiguration : IEntityTypeConfiguration<PersonalCategory>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<PersonalCategory> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Color)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Icon)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasMany(r => r.BudgetPersonalCategories)
                   .WithOne(ur => ur.PersonalCategory)
                   .HasForeignKey(ur => ur.PersonalCategoryId);
        }

        #endregion Public Methods
    }
}