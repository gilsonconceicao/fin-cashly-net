using AutoMapper;
using FinCashly.Application.Utils;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Transactions.Queries.GetTransaction
{
    public class GetTransctionMappings : Profile
    {
        public GetTransctionMappings()
        {
            CreateMap<Transaction, GetTransactionDto>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.TypeDisplay, opt => opt.MapFrom(src => src.Type.GetDescription()));
        }
    }
}
