using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionMappings : Profile
    {
        public UpdateTransactionMappings()
        {
            CreateMap<UpdateTransactionDto, Transaction>();
        }
    }
}