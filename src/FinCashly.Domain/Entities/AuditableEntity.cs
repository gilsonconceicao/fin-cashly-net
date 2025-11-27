using System.ComponentModel.DataAnnotations;
using FinCashly.Domain.Common.Interfaces;

namespace FinCashly.Domain.Entities;

public class AuditableEntity : IAuditableEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedById { get; set; } = null;
}