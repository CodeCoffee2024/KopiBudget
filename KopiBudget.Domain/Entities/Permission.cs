using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class Permission : BaseEntity
    {
        #region Internal Constructors

        internal Permission(string name, Guid moduleId)
        {
            Name = name;
            ModuleId = moduleId;
        }

        #endregion Internal Constructors

        #region Protected Constructors

        protected Permission()
        { }

        #endregion Protected Constructors

        #region Properties

        public string Name { get; private set; } = string.Empty;
        public Guid? ModuleId { get; private set; } = null;
        public virtual Module? Module { get; private set; }
        public bool IsSystemGenerated { get; private set; } = false;

        public virtual ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

        #endregion Properties

        #region Public Methods

        public static Permission Create(string name, Guid moduleId)
        {
            var entity = new Permission(name, moduleId);
            return entity;
        }

        public Permission Update(string name)
        {
            Name = name;
            return this;
        }

        public void FlagAsSystemGenerated() => IsSystemGenerated = true;

        #endregion Public Methods
    }
}