using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Exceptions;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
     private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateTransactionHandler> _logger;

    public CreateTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateTransactionHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            var payload = request.Payload;
            var account = await _uow.Accounts.GetByIdAsync(request.AccountId)
                ?? throw new NotFoundException("Conta não encontrada ou não existe"); 

            var transaction = _mapper.Map<Transaction>(payload);
            transaction.AccountId = account.Id; 
            transaction.Date = transaction.Date.ToUniversalTime(); 
            
            if (payload.CategoryId != null)
            {
                var category = await _uow.Categories.GetByIdAsync((Guid)payload.CategoryId)
                    ??  throw new NotFoundException("Categoria não encontrada ou não existe");
                transaction.CategoryId = category.Id;
            }

            await _uow.Transactions.AddAsync(transaction);
            await _uow.CommitTransactionAsync();
            return transaction.Id; 
        }
        catch(Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            _logger.LogError(ex, "Erro ao criar uma nova trasação para a conta {AccountId}", request.AccountId);
            throw;
        }
    }
};