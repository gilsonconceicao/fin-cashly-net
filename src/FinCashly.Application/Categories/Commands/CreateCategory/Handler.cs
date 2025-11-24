using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
     private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();
            
            var payload = request.Payload;
            var category = _mapper.Map<Category>(payload);

            await _uow.Categories.AddAsync(category);
            await _uow.CommitTransactionAsync();
            return category.Id; 
        }
        catch(Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            throw new Exception(ex.Message);
        }
    }
};