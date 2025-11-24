using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Categories.Commands.CreateCategory
{
    public class CreateTransactionMappings: Profile
    {
        public CreateTransactionMappings()
        {
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}