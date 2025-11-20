using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Accounts.Queries.GetAccountsList;

public class GetAccountsListHandler : IRequestHandler<GetAccountsListQuery, Paginated<GetAccountsListDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAccountsListHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Paginated<GetAccountsListDto>> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _uow.Accounts.GetGenericPaginatedList(request.Page, request.Size);
            return _mapper.Map<Paginated<GetAccountsListDto>>(list);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter listagem de contas: {ex.Message}");
        }
    }
}