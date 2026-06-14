using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Common.interfaces;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryPaginatedQuery, Paginated<GetCategoryPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryListHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMemoryCacheService _cache;
    private readonly ICacheKeysService _cacheKeys;

    public GetCategoryListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCategoryListHandler> logger, ICurrentUserService currentUserService, IMemoryCacheService cache, ICacheKeysService cacheKeys)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _cache = cache;
        _cacheKeys = cacheKeys;
    }

    public async Task<Paginated<GetCategoryPaginatedDto>> Handle(GetCategoryPaginatedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var key = _cacheKeys.Categories(_currentUserService.UserId.ToString());

            var list = await _cache.GetOrSetAsync(
                key, 
                async () => await _uow.CategoryRepository.GetCategoriesPaginatedList(_currentUserService, request.Page, request.Size), 
                TimeSpan.FromMinutes(1)
            );
            return _mapper.Map<Paginated<GetCategoryPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao consultar listagem de categorias");
            throw;
        }
    }
}