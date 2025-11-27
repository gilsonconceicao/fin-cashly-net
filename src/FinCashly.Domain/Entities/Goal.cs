using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinCashly.Domain.Entities;

public class Goal : AuditableEntity
{

    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TargetAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentAmount { get; set; } = 0;

    public DateTime? Deadline { get; set; } = null;

    public bool IsCompleted { get; set; } = false;

    // Relationships
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}