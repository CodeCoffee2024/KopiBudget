using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class Module : AuditableEntity
    {
        #region Public Constructors

        public Module(string name, string link)
        {
            Name = name;
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected Module()
        { }

        #endregion Protected Constructors

        #region Properties

        public string Name { get; private set; } = string.Empty;
        public string Link { get; private set; } = string.Empty;
        public bool IsSystemGenerated { get; private set; } = false;
        public virtual ICollection<Permission> Permissions { get; private set; } = new List<Permission>();

        #endregion Properties

        #region Public Methods

        public static Module Create(string name, string link, Guid createdById)
        {
            var module = new Module(name, link);
            module.SetCreated(createdById, DateTime.Now);
            return module;
        }

        public void FlagAsSystemGenerated() => IsSystemGenerated = true;

        public Module Update(string name, string link, Guid updatedById, DateTime updatedOn)
        {
            Name = name;
            Link = link;
            SetUpdated(updatedById, updatedOn);
            return this;
        }

        public Permission AddPermission(string permissionName, Guid? moduleId)
        {
            var permission = new Permission(permissionName, moduleId!.Value!);
            Permissions.Add(permission);
            return permission;
        }

        #endregion Public Methods
    }
}