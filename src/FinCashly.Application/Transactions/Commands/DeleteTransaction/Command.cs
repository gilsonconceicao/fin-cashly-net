using MediatR;

namespace FinCashly.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}