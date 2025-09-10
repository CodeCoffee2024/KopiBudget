using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Automappers
{
    public class TransactionProfile : Profile
    {
        #region Public Constructors

        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>().ReverseMap();
        }

        #endregion Public Constructors
    }
}