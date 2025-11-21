using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
     private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CreateTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            var payload = request.Payload;
            var account = await _uow.Accounts.GetByIdAsync(payload.AccountId)
                ?? throw new Exception("Conta não encontrada ou não existe"); 

            var transaction = _mapper.Map<Transaction>(payload);
            transaction.AccountId = account.Id; 
            transaction.Date = transaction.Date.ToUniversalTime(); 
            
            if (payload.CategoryId != null)
            {
                var category = await _uow.Categories.GetByIdAsync((Guid)payload.CategoryId)
                    ??  throw new Exception("Categoria não encontrada ou não existe");
                transaction.CategoryId = category.Id;
            }

            await _uow.Transactions.AddAsync(transaction);
            await _uow.CommitTransactionAsync();
            return transaction.Id; 
        }
        catch(Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            throw new Exception(ex.Message);
        }
    }
};