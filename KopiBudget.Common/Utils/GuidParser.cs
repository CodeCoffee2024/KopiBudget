namespace KopiBudget.Common.Utils
{
    public static class GuidParser
    {
        #region Public Methods

        /// <summary>
        /// Parses a collection of string IDs into a Guid list.
        /// Returns null if any invalid Guid is found.
        /// </summary>
        public static List<Guid>? ParseGuids(IEnumerable<string>? ids)
        {
            var guids = new List<Guid>();
            if (ids == null) return guids;

            foreach (var id in ids.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                if (!Guid.TryParse(id, out var guid)) return null;
                guids.Add(guid);
            }

            return guids;
        }

        #endregion Public Methods
    }
}