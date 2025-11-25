using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly IUnitOfWork _uow; 
    private readonly ILogger<DeleteTransactionHandler> _logger; 
    public DeleteTransactionHandler(IUnitOfWork unitOfWork, ILogger<DeleteTransactionHandler> logger)
    {
        _uow = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _uow.Transactions.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Transação não encontrada");

            await _uow.Transactions.DeleteAsync(transaction);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir uma transação existente {TransactionId}", request.Id);
            throw;
        }
    }
}