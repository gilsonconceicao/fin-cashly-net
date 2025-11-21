using AutoMapper;
using FinCashly.Application.Utils;
using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountListMappings : Profile
    {
        public GetAccountListMappings()
        {
            CreateMap<Account, GetAccountsListDto>()
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
                .ForMember(dest => dest.TypeDisplay, opt => opt.MapFrom(src => src.Type.GetDescription()));
                
            CreateMap<Paginated<Account>, Paginated<GetAccountsListDto>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
