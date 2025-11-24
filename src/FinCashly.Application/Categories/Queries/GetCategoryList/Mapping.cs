using AutoMapper;
using FinCashly.Application.Users.Queries.GetUsersList;
using FinCashly.Application.Utils;
using FinCashly.Domain.Common;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Categories.Queries.GetCategoryList
{
    public class GetCategoryListMappings : Profile
    {
        public GetCategoryListMappings()
        {
            CreateMap<Category, GetCategoryPaginatedDto>()
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
                .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
                .ForMember(dest => dest.TypeDisplay, opt => opt.MapFrom(src => src.Type.GetDescription()));

            CreateMap<Paginated<Transaction>, Paginated<GetCategoryPaginatedDto>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
