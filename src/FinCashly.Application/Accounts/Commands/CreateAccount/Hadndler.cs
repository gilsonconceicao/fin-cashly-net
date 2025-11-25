using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Accounts.Commands.CreateAccount;
#nullable disable

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAccountHandler> _logger;
    public CreateAccountHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAccountHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            var newAccount = _mapper.Map<Account>(request.Payload);
            newAccount.UserId = user.Id;

            await _unitOfWork.Accounts.AddAsync(newAccount);
            await _unitOfWork.CommitTransactionAsync();

            return newAccount.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar uma transação para o usuário {UserId}", request.UserId);
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}