using AutoMapper;
using FinCashly.Application.Accounts.Queries.GetAccounts;
using FinCashly.Application.Utils;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Accounts.Commands.CreateAccount;

public class GetAccountMappings : Profile
{
    public GetAccountMappings()
    {
        CreateMap<Account, GetAccountDto>()
            .ForMember(src => src.TypeDisplay, map => map.MapFrom(x => x.Type.GetDescription()));
    }
}