using System.ComponentModel.DataAnnotations;
namespace FinCashly.Domain.Entities;

public class User : EntityBase
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    // Relationships
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
}

// to-do: add future firebase user id
// public string UserAuthId { get; set; } = string.Empty;