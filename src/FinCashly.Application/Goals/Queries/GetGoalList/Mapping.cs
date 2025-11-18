using AutoMapper;
using FinCashly.Application.Goals.Queries.GetGoalList;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Goals.Commands.CreateAccount;

public class GetGoalMappings : Profile
{
    public GetGoalMappings()
    {
        CreateMap<Goal, GetGoalDto>();
    }
}