using FinCashly.Domain.Enums;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<UpdateCategoryHandler> _logger;
    public UpdateCategoryHandler(IUnitOfWork unitOfWork, ILogger<UpdateCategoryHandler> logger)
    {
        _uow =  unitOfWork; 
        _logger = logger;
    }
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            var category = await _uow.Categories.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Transação não encontrada");

            UpdateCategoryDto model = request.Payload; 
            
            if(!string.IsNullOrEmpty(model.Name))
                category.Name = model.Name;

            if(model.Type != null) 
                category.Type = (CategoryTypeEnum)model.Type;
            
            if(model.IsDefault != null) 
                category.IsDefault = (bool)model.IsDefault;

            await _uow.Categories.UpdateAsync(category);
            await _uow.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            _logger.LogError(ex, "Erro ao atualizar uma categoria existente {CategpryId}", request.Id);
            throw;
        }
    }
}