using AutoMapper;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Goals.Commands.UpdateGoal;
#nullable disable
public class UpdateGoalHandler : IRequestHandler<UpdateGoalCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateGoalHandler> _logger;

    public UpdateGoalHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateGoalHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var goal = await _uow.Goals.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException("Meta não encontrada");
            
            var payload = request.Payload;

            if(payload.CurrentAmount != 0 && payload.CurrentAmount > 0)
                goal.CurrentAmount = (decimal)payload.CurrentAmount;

            if (payload.TargetAmount != null && payload.TargetAmount > 0)
                goal.CurrentAmount = (decimal)payload.CurrentAmount;
                
            if (!string.IsNullOrEmpty(payload.Title))
                goal.Title = payload.Title;

            await _uow.Goals.AddAsync(goal);
            await _uow.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar uma transação {title}", request.Payload.Title);
            throw;
        }
    }
}