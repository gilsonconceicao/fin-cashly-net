using FinCashly.Application.Common.DTOs;
using FinCashly.Application.Goals.Queries.GetGoalList;
using FinCashly.Application.Transactions.Queries.GetTransaction;
using FinCashly.Domain.Enums;
#nullable disable

namespace FinCashly.Application.Categories.Queries.GetCategoryList;

public class GetCategoryPaginatedDto : EntityBaseDto
{
    public string Name { get; set; }
    public CategoryTypeEnum Type { get; set; }
    public CategoryTypeEnum TypeDisplay { get; set; }
    public bool IsDefault { get; set; }
    public List<GetTransactionDto> Transactions { get; set; } 
    public List<GetGoalDto> Goals { get; set; }
}