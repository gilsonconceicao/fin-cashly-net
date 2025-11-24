using FinCashly.Application.Utils;
using FinCashly.Domain.Enums;
using FluentValidation;

namespace FinCashly.Application.Categories.Commands.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(field => field.Name)
            .NotNull()
            .WithMessage("Nome da categoria precisa ser informado")
            .NotEmpty()
            .WithMessage("Nome da categoria não pode ser vazio");

        RuleFor(field => field.Type)
            .IsInEnum()
            .WithMessage($"Tipo não suportado, considere as opções: {StringUtils.GetAvailableValues<CategoryTypeEnum>()}");

    }
}