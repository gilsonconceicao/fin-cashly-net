using System.ComponentModel.DataAnnotations;
using FinCashly.Domain.Enums;

namespace FinCashly.Domain.Entities;

public class Category : AuditableEntity
{

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(15)]
    public CategoryTypeEnum Type { get; set; } = CategoryTypeEnum.Expense; 

    public bool IsDefault { get; set; } = false;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
}