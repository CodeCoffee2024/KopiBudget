using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class PersonalCategory : AuditableEntity
    {
        #region Properties

        public PersonalCategory()
        { }

        private PersonalCategory(string name, string icon, string color)
        {
            Name = name;
            Icon = icon;
            Color = color;
        }

        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public virtual ICollection<BudgetPersonalCategory> BudgetPersonalCategories { get; private set; } = new List<BudgetPersonalCategory>();

        public static PersonalCategory Create(string name, string icon, string color, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new PersonalCategory(name, icon, color);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(string name, string icon, string color, Guid? updatedBy, DateTime? updatedOn)
        {
            Name = name;
            Icon = icon;
            Color = color;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        #endregion Properties
    }
}