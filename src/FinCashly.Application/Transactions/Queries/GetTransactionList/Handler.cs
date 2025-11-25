using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Transactions.Queries.GetTransactionList;

public class GetTransactionListHandler : IRequestHandler<GetTransactionListQuery, Paginated<GetTransactionPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetTransactionListHandler> _logger;
    private readonly IMapper _mapper;

    public GetTransactionListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetTransactionListHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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
            _logger.LogError(ex, "Erro ao obter listagem de usuários");
            throw;
        }
    }
}