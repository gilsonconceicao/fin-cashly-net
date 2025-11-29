using System.ComponentModel;

namespace FinCashly.Domain.Security;

/// <summary>
/// Tipos de permissões
/// </summary>
public enum PermissionType
{
    /// <summary>
    /// Ler
    /// </summary>
    [Description("Ler")]

    Read = 1,

    /// <summary>
    /// Criar
    /// </summary>
    [Description("Criar")]
    Create = 2,

    /// <summary>
    /// Atualiza
    /// </summary>
    [Description("Atualiza")]
    Update = 3,

    /// <summary>
    /// Remove
    /// </summary>
    [Description("Remove")]
    Delete =4, 

    /// <summary>
    /// TESTE
    /// </summary>
    [Description("TESTE")]
    ReadOnlyTESTE =5, 
}