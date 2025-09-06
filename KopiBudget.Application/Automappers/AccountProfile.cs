using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class AccountProfile : Profile
    {
        #region Public Constructors

        public AccountProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }

        #endregion Public Constructors
    }
}