using System.ComponentModel.DataAnnotations;

namespace FinCashly.Domain.Entities;

public class EntityBase
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public string? CreatedById { get; set; }
    public bool IsDeleted { get; set; } = false;
}