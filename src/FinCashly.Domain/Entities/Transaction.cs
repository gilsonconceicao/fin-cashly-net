using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinCashly.Domain.Enums;

namespace FinCashly.Domain.Entities;

public class Transaction : EntityBase
{

    [Required]
    public Guid AccountId { get; set; }
    public Guid? CategoryId { get; set; } = null!; 

    [Required, Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required, MaxLength(15)]
    public TransactionTypeEnum Type { get; set; } = TransactionTypeEnum.Expense;

    [MaxLength(255)]
    public string? Description { get; set; }

    public DateTime Date { get; set; }

    // Relationships
    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; } = null!;

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; } = null!;
}