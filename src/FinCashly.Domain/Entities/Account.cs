using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinCashly.Domain.Enums;

namespace FinCashly.Domain.Entities;

public class Account : AuditableEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public AccountTypeEnum Type { get; set; } = AccountTypeEnum.Checking;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } = 0;

    // Relationships
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}