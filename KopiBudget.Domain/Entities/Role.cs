using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class Role : AuditableEntity
    {
        #region Private Constructors

        protected Role()
        { }

        #endregion Private Constructors

        #region Properties

        private Role(string name)
        {
            Name = name;
        }

        public string Name { get; private set; } = string.Empty;
        public bool IsSystemGenerated { get; private set; } = false;

        public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

        public static Role Create(string name, Guid createdById)
        {
            var entity = new Role(name);
            entity.SetCreated(createdById, DateTime.Now);
            return entity;
        }

        public Role Update(string name, Guid updatedById)
        {
            Name = name;
            SetUpdated(updatedById, DateTime.Now);
            return this;
        }

        public void FlagAsSystemGenerated() => IsSystemGenerated = true;

        public void AddPermission(Permission permission)
        {
            RolePermissions.Add(new RolePermission { Role = this, Permission = permission });
        }

        #endregion Properties
    }
}