using MediatR;

namespace FinCashly.Application.Users.Queries.GetUsersList; 
public record GetUsersListQuery(
    int page = 1, 
    int size = 5
) : IRequest<List<string>>; 