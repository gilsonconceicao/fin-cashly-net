using MediatR;

namespace FinCashly.Application.Categories.Commands.CreateCategory;
#nullable disable
public class CreateCategoryCommand : IRequest<Guid>
{
    public CreateCategoryDto Payload { get; set; }
};