using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
#nullable disable

namespace FinCashly.Application.Users.Commands.UpdateUser;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = await _uow.Users.GetByIdAsync(request.Id); 

            if (user == null)
            {
                throw new NotFoundException("Usuário não encontrado");
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar um usuário existente {userId}", request.Id);
            throw;
        }
    }
}