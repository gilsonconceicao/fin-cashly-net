using MediatR;

namespace FinCashly.Application.Accounts.Commands.CreateAccount; 
#nullable disable

public class CreateAccountCommand : IRequest<Guid>
{
    public Guid UserId {get; set;}
    public CreateAccountDto Payload {get; set;}    
}