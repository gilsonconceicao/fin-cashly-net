using FinCashly.Domain.Enums;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Accounts.Commands.UpdateAccount;
#nullable disable

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateAccountHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId); 
            
            if (account == null)
            {
                throw new Exception("Conta não encontrado");
            }
            var payload = request.Payload; 
            
            if (payload.Balance != null && payload.Balance is decimal)
                account.Balance = (decimal)payload.Balance; 
            
            if (payload.Name != null)
                account.Name = payload.Name; 
            
            if (payload.Type != null)
                account.Type = (AccountTypeEnum)payload.Type; 
            

            await _unitOfWork.SaveChangesAsync(); 
            return account.Id;
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}