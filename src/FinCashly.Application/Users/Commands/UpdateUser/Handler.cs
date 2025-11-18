using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;
#nullable disable

namespace FinCashly.Application.Users.Commands.UpdateUser;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IUnitOfWork _uow;

    public UpdateUserHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = await _uow.Users.GetByIdAsync(request.Id); 

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            
            var payload = request.Payload;

            if (!string.IsNullOrEmpty(payload.Name))
                user.Name = payload.Name;

            if (!string.IsNullOrEmpty(payload.Email))
                user.Email = payload.Email;
            
            await _uow.Users.UpdateAsync(user);

            await _uow.SaveChangesAsync();
            return user.Id;
        }
        catch (Exception exception)
        {
            throw new Exception($"Erro ao criar o usuário: {exception.Message}");
        }
    }
}