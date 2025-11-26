using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Goals.Queries.GetGoalList;
using FinCashly.Domain.Common;
using MediatR;

namespace FinCashly.Application.Goals.Queries.GetGoalList; 
public class GetGoalPaginatedListQuery : QueryParamsQuery, IRequest<Paginated<GetGoalPaginatedDto>>
{
} 