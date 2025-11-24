using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Common;
using MediatR;

namespace FinCashly.Application.Categories.Queries.GetCategoryList; 
public class GetCategoryQuery : QueryParamsQuery, IRequest<Paginated<GetCategoryPaginatedDto>>
{
} 