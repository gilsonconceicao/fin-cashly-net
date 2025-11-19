using AutoMapper;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Users.Commands.DeleteUser;
#nullable disable
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public DeleteUserHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _uow.Users.GetByIdAsync(request.Id)
                                        ?? throw new Exception("Usuário não encontrado ou não existe");
            await _uow.Users.DeleteForEverAsync(user);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            throw new Exception($"Erro ao criar o usuário: {exception.Message}");
        }
    }
}