using MediatR;

namespace FinCashly.Application.Goals.Commands.UpdateGoal;
#nullable disable
public class UpdateGoalCommand : IRequest<bool>
{
    public Guid Id {get; set;}
    public UpdateGoalDto Payload {get; set;}
}