using MediatR;
#nullable disable
namespace FinCashly.Application.Transactions.Commands.UpdateTransaction;
public class UpdateTransactionCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public UpdateTransactionDto Payload { get; set; }

}