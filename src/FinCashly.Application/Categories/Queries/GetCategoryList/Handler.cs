using AutoMapper;
using FinCashly.Domain.Common;
using FinCashly.Domain.Repositories;
using MediatR;

namespace FinCashly.Application.Categories.Queries.GetCategoryList;

public class GetCategoryListHandler : IRequestHandler<GetCategoryQuery, Paginated<GetCategoryPaginatedDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetCategoryListHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _uow = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Paginated<GetCategoryPaginatedDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
         try
        {
            var list = await _uow.Categories.GetCategoriesPaginatedList(request.Page, request.Size);
            return _mapper.Map<Paginated<GetCategoryPaginatedDto>>(list);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter listagem: {ex.Message}");
        }
    }
}