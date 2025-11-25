using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Users.Commands.CreateUser;
#nullable disable
public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;


    public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            _logger.LogError(ex, "Erro ao criar o usuário {Name}", request.Payload.Name);
            throw;
        }
    }
}