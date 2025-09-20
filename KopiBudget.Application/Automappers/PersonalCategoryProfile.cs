using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class PersonalCategoryProfile : Profile
    {
        #region Public Constructors

        public PersonalCategoryProfile()
        {
            CreateMap<PersonalCategoryDto, PersonalCategory>().ReverseMap();
            CreateMap<PersonalCategoryDropdownDto, PersonalCategory>().ReverseMap();
        }

        #endregion Public Constructors
    }
}