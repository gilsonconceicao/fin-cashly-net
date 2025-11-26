using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Goals.Commands.DeleteGoal;

public class DeleteGoalsHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly IUnitOfWork _uow; 
    private readonly ILogger<DeleteGoalsHandler> _logger; 
    public DeleteGoalsHandler(IUnitOfWork unitOfWork, ILogger<DeleteGoalsHandler> logger)
    {
        _uow = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _uow.Goals.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("meta não encontrada");

            await _uow.Goals.DeleteAsync(transaction);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir uma meta existente {GoalsId}", request.Id);
            throw;
        }
    }
}