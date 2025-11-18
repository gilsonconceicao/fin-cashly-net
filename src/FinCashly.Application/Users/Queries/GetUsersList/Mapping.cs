using AutoMapper;
using FinCashly.Application.Users.Queries.GetUsersList;
using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Users.Commands.CreateUser
{
    public class GetUserPaginatedMapping : Profile
    {
        public GetUserPaginatedMapping()
        {
            CreateMap<User, GetUserPaginatedDto>()
                .ForMember(dest => dest.Accounsts, opt => opt.MapFrom(src => src.Accounts))
                .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals));

            CreateMap<Paginated<User>, Paginated<GetUserPaginatedDto>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
