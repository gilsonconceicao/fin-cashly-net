using FinCashly.Application.Common.DTOs;
using FinCashly.Domain.Common;
using MediatR;

namespace FinCashly.Application.Users.Queries.GetUsersList; 
public class GetUsersListQuery : IRequest<Paginated<GetUserPaginatedDto>>
{
    public int Page {get; set; } = 0; 
    public int Size {get; set; } = 5; 
} 