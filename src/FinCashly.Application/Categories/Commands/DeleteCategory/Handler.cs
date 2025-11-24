using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _uow; 
    public DeleteCategoryHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _uow.Categories.GetByIdAsync(request.Id)
                ?? throw new Exception("Categoria não encontrada");

            await _uow.Categories.DeleteAsync(transaction);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}