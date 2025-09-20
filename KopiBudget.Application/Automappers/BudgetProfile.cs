using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class BudgetProfile : Profile
    {
        #region Public Constructors

        public BudgetProfile()
        {
            CreateMap<Budget, BudgetDto>();
            CreateMap<Budget, BudgetDropdownDto>().ReverseMap();
            CreateMap<BudgetPersonalCategory, BudgetPersonalCategoryDto>();
            CreateMap<PersonalCategory, PersonalCategoryFragment>();
        }

        #endregion Public Constructors
    }
}