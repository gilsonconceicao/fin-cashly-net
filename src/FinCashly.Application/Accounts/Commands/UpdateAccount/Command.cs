using MediatR;

namespace FinCashly.Application.Accounts.Commands.UpdateAccount; 
#nullable disable

public class UpdateAccountCommand : IRequest<Guid>
{
    public Guid AccountId {get; set;}
    public UpdateAccountDto Payload {get; set;}    
}