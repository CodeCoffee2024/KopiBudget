using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class CategoryProfile : Profile
    {
        #region Public Constructors

        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDropdownDto>().ReverseMap();
        }

        #endregion Public Constructors
    }
}