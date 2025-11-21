using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Common;
using MediatR;

namespace FinCashly.Application.Transactions.Queries.GetTransactionList; 
public class GetTransactionListQuery : QueryParamsQuery, IRequest<Paginated<GetTransactionPaginatedDto>>
{
} 