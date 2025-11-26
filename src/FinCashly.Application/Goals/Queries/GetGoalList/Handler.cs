using AutoMapper;
using FinCashly.Application.Goals.Queries.GetGoalList;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Queries.GetCategoryList;

public class GetGoalListHandler : IRequestHandler<GetGoalPaginatedListQuery, Paginated<GetGoalPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetGoalListHandler> _logger;

    public GetGoalListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetGoalListHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Paginated<GetGoalPaginatedDto>> Handle(GetGoalPaginatedListQuery request, CancellationToken cancellationToken)
    {
         try
        {
            var list = await _uow.Goals.GetGoalsPaginatedList(request.Page, request.Size);
            return _mapper.Map<Paginated<GetGoalPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao consultar listagem de categorias");
            throw;
        }
    }
}