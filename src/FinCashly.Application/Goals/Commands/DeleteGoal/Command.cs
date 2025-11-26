using MediatR;

namespace FinCashly.Application.Goals.Commands.DeleteGoal;

public class DeleteGoalCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}