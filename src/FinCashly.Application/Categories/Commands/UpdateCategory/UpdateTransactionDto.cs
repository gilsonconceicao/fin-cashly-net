using FinCashly.Domain.Enums;

namespace FinCashly.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryDto
{
    public string? Name { get; set; } = null;
    public CategoryTypeEnum? Type { get; set; } = null;
    public bool? IsDefault { get; set; } = null;
}