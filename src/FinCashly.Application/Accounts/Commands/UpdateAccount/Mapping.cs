using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Accounts.Commands.UpdateAccount;

public class CreateAccountMapping : Profile
{
    public CreateAccountMapping()
    {
        CreateMap<UpdateAccountByUserDto, Account>();
    }
}