using MediatR;
#nullable disable

namespace FinCashly.Application.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public UpdateCategoryDto Payload { get; set; }

}