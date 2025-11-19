using AutoMapper;
using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Accounts.Commands.CreateAccount;

public class CreateAccountMapping : Profile
{
    public CreateAccountMapping()
    {
        CreateMap<CreateAccountDto, Account>();
    }
}