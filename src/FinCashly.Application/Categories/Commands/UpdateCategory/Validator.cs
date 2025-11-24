using FinCashly.Application.Utils;
using FinCashly.Domain.Enums;
using FluentValidation;

namespace FinCashly.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(field => field.Type)
            .IsInEnum()
            .WithMessage($"Tipo não suportado, considere as opções: {StringUtils.GetAvailableValues<CategoryTypeEnum>()}");

    }
}