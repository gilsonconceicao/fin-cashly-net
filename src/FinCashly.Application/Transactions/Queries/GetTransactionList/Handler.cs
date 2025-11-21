using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Transactions.Queries.GetTransactionList;

public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, Paginated<GetTransactionPaginatedDto>>
{
       private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetTransactionListHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Paginated<GetTransactionPaginatedDto>> Handle(GetTransactionListQuery request, CancellationToken cancellationToken)
    {
         try
        {
            var list = await _uow.Transactions.GetTransactionPaginated(request.Page, request.Size);
            return _mapper.Map<Paginated<GetTransactionPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter listagem de usuários: {ex.Message}");
        }
    }
}