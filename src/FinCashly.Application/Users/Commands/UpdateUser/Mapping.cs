using AutoMapper;
using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Users.Commands.UpdateUser;
using FinCashly.Domain.Entities;
namespace FinCashly.Application.Users.Commands.CreateUser
{
    public class UpdateUserMapping : Profile
    {
        public UpdateUserMapping()
        {
            CreateMap<UpdateUserDto, User>();
        }
    }
}
