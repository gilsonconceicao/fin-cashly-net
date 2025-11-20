using AutoMapper;
using FinCashly.Domain.Entities;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Accounts.Commands.CreateAccount;
#nullable disable

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId); 
            
            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            
            var newAccount = _mapper.Map<Account>(request.Payload);
            newAccount.UserId = user.Id;

            await _unitOfWork.Accounts.AddAsync(newAccount);
            await _unitOfWork.CommitTransactionAsync(); 

            return newAccount.Id;
        }
        catch (Exception exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new Exception(exception.Message);
        }
    }
}