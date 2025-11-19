using AutoMapper;
using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Goals.Commands.CreateAccount;

public class CreateGoalMapping : Profile
{
    public CreateGoalMapping()
    {
        CreateMap<CreateGoalDto, Goal>();
    }
}