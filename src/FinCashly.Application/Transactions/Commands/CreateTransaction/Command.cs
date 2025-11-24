using MediatR;

namespace FinCashly.Application.Transactions.Commands.CreateTransaction;
#nullable disable
public class CreateTransactionCommand : IRequest<Guid>
{
    public Guid AccountId { get; set; }
    public CreateTransactionDto Payload { get; set; }
};