namespace KopiBudget.Application.Dtos
{
    public record CategoryDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        #endregion Properties
    }
}