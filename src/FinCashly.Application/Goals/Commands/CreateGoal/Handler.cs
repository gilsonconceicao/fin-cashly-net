using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Goals.Commands.CreateGoal;
#nullable disable
public class CreateGoalHandler : IRequestHandler<CreateGoalCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateGoalHandler> _logger;

    public CreateGoalHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateGoalHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            var goal = _mapper.Map<Goal>(request.Payload);
            await _uow.Goals.AddAsync(goal); 
            await _uow.CommitTransactionAsync(); 
            
            return goal.Id;
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            _logger.LogError(ex, "Erro ao criar uma transação {title}", request.Payload.Title); 
            throw; 
        }
    }
}