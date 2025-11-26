using FinCashly.Application.Goals.Queries.GetGoal;
using FinCashly.Application.Transactions.Queries.GetTransaction;
using FinCashly.Domain.Enums;

#nullable disable
namespace FinCashly.Application.Categories.Queries.GetCategory;
public class GetCategory
{
    public string Name { get; set; }
    public CategoryTypeEnum Type { get; set; }
    public CategoryTypeEnum TypeDisplay { get; set; }
    public bool IsDefault { get; set; }
    public List<GetTransactionDto> Transactions { get; set; }
    public List<GetGoalDto> Goals { get; set; }
}