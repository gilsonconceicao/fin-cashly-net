using AutoMapper;
using FinCashly.Application.Utils;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Categories.Queries.GetCategory
{
    public class GetCategoryMappings : Profile
    {
        public GetCategoryMappings()
        {
            CreateMap<Category, GetCategory>()
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
                .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Goals))
                .ForMember(dest => dest.TypeDisplay, opt => opt.MapFrom(src => src.Type.GetDescription()));
        }
    }
}
