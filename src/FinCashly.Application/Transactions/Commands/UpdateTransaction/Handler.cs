using FinCashly.Domain.Enums;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public UpdateTransactionHandler(IUnitOfWork unitOfWork)
    {
        _uow =  unitOfWork; 
    }
    public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            var transaction = await _uow.Transactions.GetByIdAsync(request.Id)
                ?? throw new Exception("Transação não encontrada");

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
                    throw new Exception("Categoria informada não encontrada");
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
            throw new Exception(ex.Message);
        }
    }
}