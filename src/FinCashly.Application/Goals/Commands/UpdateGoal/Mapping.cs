using AutoMapper;
using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Goals.Commands.UpdateGoal;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Goals.Commands.CreateAccount;

public class UpdateGoalMappings : Profile
{
    public UpdateGoalMappings()
    {
        CreateMap<UpdateGoalByUserDto, Goal>();
    }
}