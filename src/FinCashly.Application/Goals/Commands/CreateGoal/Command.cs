using MediatR;

namespace FinCashly.Application.Goals.Commands.CreateGoal;
#nullable disable
public class CreateGoalCommand : IRequest<Guid>
{
    public CreateGoalDto Payload {get; set;}
}