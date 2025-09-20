using KopiBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KopiBudget.Infrastructure.Configuration
{
    public class BudgetPersonalCategoryConfiguration : IEntityTypeConfiguration<BudgetPersonalCategory>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<BudgetPersonalCategory> builder)
        {
            builder.ToTable("BudgetPersonalCategories");

            builder.HasKey(ur => new { ur.BudgetId, ur.PersonalCategoryId });

            builder.HasOne(ur => ur.Budget)
                   .WithMany(u => u.BudgetPersonalCategories)
                   .HasForeignKey(ur => ur.BudgetId);

            builder.HasOne(ur => ur.PersonalCategory)
                   .WithMany(r => r.BudgetPersonalCategories)
                   .HasForeignKey(ur => ur.PersonalCategoryId);
        }

        #endregion Public Methods
    }
}