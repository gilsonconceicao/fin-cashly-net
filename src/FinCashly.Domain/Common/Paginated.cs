namespace FinCashly.Domain.Common;
public class Paginated<T>
{
    /// <summary>
    /// Página atual
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Quantidade de itens
    /// </summary>
    public int? TotalItems { get; set; }

    /// <summary>
    /// Quantidade de página
    /// </summary>
    public int? TotalPages { get; set; }

    /// <summary>
    /// Listagem 
    /// </summary>
    public List<T> Data { get; set; } = new List<T>();
}