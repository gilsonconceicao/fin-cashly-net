using System.ComponentModel.DataAnnotations;

namespace FinCashly.Domain.Entities;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? UpdateAt { get; set; } = null;
    public string? CreatedById { get; set; } = null;
}