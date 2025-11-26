using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Goals.Queries.GetGoalList
{
    public class GetGoalsPaginatedMappings : Profile
    {
        public GetGoalsPaginatedMappings()
        {
            CreateMap<Goal, GetGoalPaginatedDto>();

            CreateMap<Paginated<Goal>, Paginated<GetGoalPaginatedDto>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
