namespace KopiBudget.Common.Constants
{
    public static class ModuleGroups
    {
        #region Fields

        public const string DASHBOARD = "Dashboard";
        public const string CONTENT = "Content";
        public const string SYSTEM = "System";

        public static readonly Dictionary<string, string[]> GROUPS = new()
        {
            { DASHBOARD, new[] { Modules.DASHBOARD } },
            { CONTENT, new[] { Modules.ACCOUNT, Modules.BUDGET, Modules.TRASACTION } },
            { SYSTEM, new[] { Modules.USER, Modules.ROLE, Modules.CATEGORY } }
        };

        #endregion Fields
    }
}