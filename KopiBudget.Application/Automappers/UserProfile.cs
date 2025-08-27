using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class UserProfile : Profile
    {
        #region Public Constructors

        public UserProfile()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }

        #endregion Public Constructors
    }
}