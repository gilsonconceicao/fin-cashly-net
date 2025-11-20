using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Accounts.Commands.DeleteAccount;
#nullable disable
public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public DeleteAccountHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _uow.Accounts.GetByIdAsync(request.Id)
                                        ?? throw new Exception("Conta não encontrado ou não existe");
            await _uow.Accounts.DeleteAsync(account);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch (Exception exception)
        {
            throw new Exception($"Erro ao excluir uma conta: {exception.Message}");
        }
    }
}