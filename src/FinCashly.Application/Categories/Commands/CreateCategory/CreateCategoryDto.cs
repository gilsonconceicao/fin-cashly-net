using FinCashly.Application.Transactions.Commands.CreateTransaction;
using FinCashly.Domain.Enums;

namespace FinCashly.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public CategoryTypeEnum Type { get; set; } = CategoryTypeEnum.Expense;
        public bool IsDefault { get; set; } = false;
    }
}