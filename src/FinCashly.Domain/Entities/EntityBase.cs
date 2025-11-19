using System.ComponentModel.DataAnnotations;

namespace FinCashly.Domain.Entities;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedById { get; set; } = null;
}