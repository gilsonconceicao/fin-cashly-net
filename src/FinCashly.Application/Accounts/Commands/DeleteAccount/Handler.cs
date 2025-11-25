using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Accounts.Commands.DeleteAccount;
#nullable disable
public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<DeleteAccountHandler> _logger;
    public DeleteAccountHandler(IUnitOfWork unitOfWork, ILogger<DeleteAccountHandler> logger)
    {
        _uow = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _uow.Accounts.GetByIdAsync(request.Id)
                                        ?? throw new NotFoundException("Conta não encontrado ou não existe");
            await _uow.Accounts.DeleteAsync(account);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir uma conta {AccountId}", request.Id);
            throw;
        }
    }
}