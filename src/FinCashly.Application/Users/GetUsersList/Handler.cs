using MediatR;

namespace FinCashly.Application.Users.GetUsersList;

public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, List<string>>
{
    public Task<List<string>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        var listTeste = new List<string>
        {
            "Test", 
            "MediatoR Working"
        };

        return Task.FromResult(listTeste);
    }
}