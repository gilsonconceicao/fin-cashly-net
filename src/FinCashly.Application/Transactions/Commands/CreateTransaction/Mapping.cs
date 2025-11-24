using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionMappings: Profile
    {
        public CreateTransactionMappings()
        {
            CreateMap<CreateTransactionDto, Transaction>();
        }
    }
}