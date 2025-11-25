using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Accounts.Queries.GetAccountsList;

public class GetAccountsListHandler : IRequestHandler<GetAccountsListQuery, Paginated<GetAccountsListDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAccountsListHandler> _logger;

    public GetAccountsListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAccountsListHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Paginated<GetAccountsListDto>> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _uow.Accounts.GetAccountsPaginated(request.Page, request.Size);
            return _mapper.Map<Paginated<GetAccountsListDto>>(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter lista de contas");
            throw;
        }
    }
}