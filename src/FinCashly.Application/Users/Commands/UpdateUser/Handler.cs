using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;
#nullable disable

namespace FinCashly.Application.Users.Commands.UpdateUser;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
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

            if (payload.Accounsts != null && payload.Accounsts.Count > 0)
            {
                user.Accounts = _mapper.Map<List<Account>>(payload.Accounsts);
            }
            if (payload.Goals != null && payload.Goals.Count > 0)
            {
                user.Goals = _mapper.Map<List<Goal>>(payload.Goals);
            }
            
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