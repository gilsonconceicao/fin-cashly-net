using AutoMapper;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Users.Commands.DeleteUser;
#nullable disable
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<DeleteUserHandler> _logger;

    public DeleteUserHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserHandler> logger)
    {
        _uow = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _uow.Users.GetByIdAsync(request.Id)
                                        ?? throw new NotFoundException("Usuário não encontrado ou não existe");
            await _uow.Users.DeleteForEverAsync(user);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir um usuário existente {UserId}", request.Id);
            throw;
        }
    }
}