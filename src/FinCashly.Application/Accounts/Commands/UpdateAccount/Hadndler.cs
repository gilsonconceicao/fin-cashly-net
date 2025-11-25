using FinCashly.Domain.Enums;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Accounts.Commands.UpdateAccount;
#nullable disable

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAccountHandler> _logger;
    public UpdateAccountHandler(IUnitOfWork unitOfWork, ILogger<UpdateAccountHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId);

            if (account == null)
            {
                throw new NotFoundException("Conta não encontrada");
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar uma conta {AccountId}", request.AccountId);
            throw;
        }
    }
}