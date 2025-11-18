using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Users.Queries.GetUsersList;

public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, Paginated<GetUserPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetUsersListHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Paginated<GetUserPaginatedDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var list = await _uow.Users.GetUsersPaginatedList(request.Page, request.Size);
            return _mapper.Map<Paginated<GetUserPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter listagem de usuários: {ex.Message}");
        }
    }
}