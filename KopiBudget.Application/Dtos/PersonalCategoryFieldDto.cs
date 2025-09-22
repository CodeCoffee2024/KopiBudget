namespace KopiBudget.Application.Dtos
{
    public record PersonalCategoryFieldDto
    {
        public IEnumerable<PersonalCategoryColorDto> Colors { get; set; } = Enumerable.Empty<PersonalCategoryColorDto>();
        public IEnumerable<PersonalCategoryIconDto> Icons { get; set; } = Enumerable.Empty<PersonalCategoryIconDto>();
    }
}