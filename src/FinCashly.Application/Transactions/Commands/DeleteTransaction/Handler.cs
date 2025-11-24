using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly IUnitOfWork _uow; 
    public DeleteTransactionHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _uow.Transactions.GetByIdAsync(request.Id)
                ?? throw new Exception("Transação não encontrada");

            await _uow.Transactions.DeleteAsync(transaction);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}