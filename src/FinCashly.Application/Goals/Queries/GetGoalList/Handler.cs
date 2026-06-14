using AutoMapper;
using FinCashly.Application.Goals.Queries.GetGoalList;
using FinCashly.Domain.Common;
using FinCashly.Domain.Common.interfaces;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Queries.GetCategoryList;

public class GetGoalListHandler : IRequestHandler<GetGoalPaginatedListQuery, Paginated<GetGoalPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetGoalListHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMemoryCacheService _cache;
    private readonly ICacheKeysService _cacheKeys;

    public GetGoalListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetGoalListHandler> logger, ICurrentUserService currentUserService, IMemoryCacheService cache, ICacheKeysService cacheKeys)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _cache = cache;
        _cacheKeys = cacheKeys; 
    }

    public async Task<Paginated<GetGoalPaginatedDto>> Handle(GetGoalPaginatedListQuery request, CancellationToken cancellationToken)
    {
         try
        {
            var key = _cacheKeys.Goals(_currentUserService.UserId); 
            var list = await _cache.GetOrSetAsync(
                key, 
                async () => await _uow.GoalRepository.GetGoalsPaginatedList(_currentUserService, request.Page, request.Size), 
                TimeSpan.FromSeconds(30)
            );
            return _mapper.Map<Paginated<GetGoalPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao consultar listagem de categorias");
            throw;
        }
    }
}