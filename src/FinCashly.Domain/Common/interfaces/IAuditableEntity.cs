namespace FinCashly.Domain.Common.Interfaces;

public interface IAuditableEntity
{
    Guid Id { get; set; }
    bool IsDeleted { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    string? CreatedById { get; set; }
}