using FinCashly.Domain.Enums;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<UpdateTransactionHandler> _logger;
    public UpdateTransactionHandler(IUnitOfWork unitOfWork, ILogger<UpdateTransactionHandler> logger)
    {
        _uow =  unitOfWork; 
        _logger = logger;
    }
    public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            var transaction = await _uow.Transactions.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Transação não encontrada");

            UpdateTransactionDto model = request.Payload; 
            
            if(model.Amount != null && model.Amount > 0)
            {
                transaction.Amount = model.Amount.Value;
            }

            if(model.Type != null) 
                transaction.Type = (TransactionTypeEnum)model.Type;
            
            if(model.Description != null) 
                transaction.Description = model.Description;
            
            if (model.CategoryId != null)
            {
                if (await _uow.Categories.GetByIdAsync((Guid)model.CategoryId) is null)
                {
                    throw new NotFoundException("Categoria informada não encontrada");
                }

                transaction.CategoryId = model.CategoryId;
            }

            await _uow.Transactions.UpdateAsync(transaction);
            await _uow.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            _logger.LogError(ex, "Erro ao atualizar uma transação exisitente {TransactionId}", request.Id);
            throw;
        }
    }
}