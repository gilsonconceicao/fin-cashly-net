using AutoMapper;
using FinCashly.Application.Categories.Commands.UpdateCategory;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Transactions.Commands.UpdateTransaction
{
    public class UpdateCategoryMappings : Profile
    {
        public UpdateCategoryMappings()
        {
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}