namespace FinCashly.Application.Common.DTOs;

public class EntityBaseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}