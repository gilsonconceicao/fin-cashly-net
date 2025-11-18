using AutoMapper;
using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Entities;
namespace FinCashly.Application.Users.Commands.CreateUser; 

public class CreateUserMapping : Profile
{
    public CreateUserMapping()
    {
        CreateMap<CreateUserDto, User>();
    }
}