using MediatR;

namespace FinCashly.Application.Accounts.Commands.DeleteAccount;
public class DeleteAccountCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}