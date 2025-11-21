using AutoMapper;
using FinCashly.Application.Users.Queries.GetUsersList;
using FinCashly.Application.Utils;
using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Transactions.Queries.GetTransactionList
{
    public class GetTransactionListMappings : Profile
    {
        public GetTransactionListMappings()
        {
            CreateMap<Transaction, GetTransactionPaginatedDto>()
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.TypeDisplay, opt => opt.MapFrom(src => src.Type.GetDescription()));

            CreateMap<Paginated<Transaction>, Paginated<GetTransactionPaginatedDto>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
