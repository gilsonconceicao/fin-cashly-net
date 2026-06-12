using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinCashly.Domain.Enums;

namespace FinCashly.Domain.Entities;

public class Account : AuditableEntity
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public AccountTypeEnum Type { get; set; } = AccountTypeEnum.Checking;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } = 0;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}