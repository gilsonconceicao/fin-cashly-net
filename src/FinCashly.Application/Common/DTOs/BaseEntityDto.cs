namespace FinCashly.Application.Common.DTOs;

public class BaseEntityDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
}