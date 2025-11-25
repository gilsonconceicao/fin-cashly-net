using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _uow; 
    private readonly ILogger<DeleteCategoryHandler> _logger; 
    public DeleteCategoryHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategoryHandler> logger)
    {
        _uow = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _uow.Categories.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Categoria não encontrada");

            await _uow.Categories.DeleteAsync(transaction);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir uma categoria existente {CategpryId}", request.Id);
            throw;
        }
    }
}