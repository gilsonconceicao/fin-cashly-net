using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Users.Commands.CreateUser;
#nullable disable
public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var payload = request.Payload;
            User user = _mapper.Map<User>(payload);
            
            await _uow.BeginTransactionAsync();

            await _uow.Users.AddAsync(user);

            if (payload.Accounsts.Count > 0)
            {
                foreach (var account in payload.Accounsts)
                {
                    user.Accounts.Add(_mapper.Map<Account>(account));
                }
            }

            if (payload.Goals.Count > 0)
            {
                foreach (var goal in payload.Goals)
                {
                    user.Goals.Add(_mapper.Map<Goal>(goal));
                }
            }

            await _uow.CommitTransactionAsync();
            return user.Id;
        }
        catch (Exception exception)
        {
            await _uow.RollbackTransactionAsync();
            throw new Exception($"Erro ao criar o usuário: {exception.Message}");
        }
    }
}