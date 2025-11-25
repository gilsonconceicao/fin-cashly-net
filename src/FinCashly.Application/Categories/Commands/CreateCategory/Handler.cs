using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
     private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCategoryHandler> _logger;

    public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCategoryHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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
            _logger.LogError(ex, "Erro ao criar uma nova categoria {Name}", request.Payload.Name);
            throw;
        }
    }
};