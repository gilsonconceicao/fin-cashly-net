using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Users.Queries.GetUsersList;

public class GetUsersListHandler : IRequestHandler<GetUsersListQuery, Paginated<GetUserPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUsersListHandler> _logger;

    public GetUsersListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetUsersListHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
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
            _logger.LogError(ex, "Erro ao obter listagem de usuários");
            throw;
        }
    }
}