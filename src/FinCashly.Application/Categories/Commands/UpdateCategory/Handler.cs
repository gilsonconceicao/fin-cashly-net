using FinCashly.Domain.Enums;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public UpdateCategoryHandler(IUnitOfWork unitOfWork)
    {
        _uow =  unitOfWork; 
    }
    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            var category = await _uow.Categories.GetByIdAsync(request.Id)
                ?? throw new Exception("Transação não encontrada");

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
            throw new Exception(ex.Message);
        }
    }
}