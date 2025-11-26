using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Goals.Queries.GetGoal;

public class GetGoalMappings : Profile
{
    public GetGoalMappings()
    {
        CreateMap<Goal, GetGoalDto>();
    }
}