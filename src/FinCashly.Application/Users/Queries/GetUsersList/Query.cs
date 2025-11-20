using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Common;
using MediatR;

namespace FinCashly.Application.Users.Queries.GetUsersList; 
public class GetUsersListQuery : QueryParamsQuery, IRequest<Paginated<GetUserPaginatedDto>>
{
} 