using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Common.Constants;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Infrastructure.Services
{
    public class SystemSettingsService(IModuleRepository moduleRepository) : ISystemSettingsService
    {
        #region Public Methods

        public IEnumerable<ModuleGroupDto> GetAllModuleGroups()
        {
            return ModuleGroups.GROUPS.Select(group => new ModuleGroupDto
            {
                Key = group.Key,
                Modules = moduleRepository.GetByModuleNames(group.Value)
                .Result.Select(m => new ModuleDto
                {
                    Id = m.Id!.Value,
                    Name = m.Name,
                    Link = m.Link
                }).ToList()
            });
        }

        public IEnumerable<CurrencyDto> GetAllCurrencies()
        {
            return Currencies.LIST.Select(c => new CurrencyDto
            {
                Code = c.Key,
                Description = c.Value
            });
        }

        public PersonalCategoryFieldDto GetAllPersonalCategoryFields()
        {
            return new PersonalCategoryFieldDto
            {
                Colors = PersonalCategories.COLORSLIST.Select(c => new PersonalCategoryColorDto
                {
                    Key = c.Key,
                    Value = c.Value
                }).ToList(),

                Icons = PersonalCategories.ICONSLIST.Select(c => new PersonalCategoryIconDto
                {
                    Key = c.Key,
                    Value = c.Value
                }).ToList()
            };
        }

        #endregion Public Methods
    }
}