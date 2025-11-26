using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinCashly.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryPaginatedQuery, Paginated<GetCategoryPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCategoryListHandler> _logger;

    public GetCategoryListHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCategoryListHandler> logger)
    {
        _uow = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Paginated<GetCategoryPaginatedDto>> Handle(GetCategoryPaginatedQuery request, CancellationToken cancellationToken)
    {
         try
        {
            var list = await _uow.Categories.GetCategoriesPaginatedList(request.Page, request.Size);
            return _mapper.Map<Paginated<GetCategoryPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao consultar listagem de categorias");
            throw;
        }
    }
}