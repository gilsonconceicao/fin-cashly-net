using AutoMapper;
using FinCashly.Domain.Common.interfaces;
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
    private readonly ICurrentUserService _currentUserService; 
    public CreateAccountHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAccountHandler> logger, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userContext = _currentUserService;

            await _unitOfWork.BeginTransactionAsync();
    
            var newAccount = _mapper.Map<Account>(request.Payload);

            await _unitOfWork.AccountRepository.AddAsync(newAccount);
            await _unitOfWork.CommitTransactionAsync();

            return newAccount.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar uma transação para o usuário {UserId}", _currentUserService.UserId);
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}