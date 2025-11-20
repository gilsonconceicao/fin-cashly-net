using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Common;
using MediatR;

namespace FinCashly.Application.Accounts.Queries.GetAccountsList; 

public class GetAccountsListQuery : QueryParamsQuery, IRequest<Paginated<GetAccountsListDto>>
{
} 
