using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Goals.Commands.CreateGoal;

public class CreateGoalMapping : Profile
{
    public CreateGoalMapping()
    {
        CreateMap<CreateGoalDto, Goal>();
    }
}