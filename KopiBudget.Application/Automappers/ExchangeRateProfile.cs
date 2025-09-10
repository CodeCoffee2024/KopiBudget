using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class ExchangeRateProfile : Profile
    {
        #region Public Constructors

        public ExchangeRateProfile()
        {
            CreateMap<ExchangeRateDto, ExchangeRate>().ReverseMap();
        }

        #endregion Public Constructors
    }
}