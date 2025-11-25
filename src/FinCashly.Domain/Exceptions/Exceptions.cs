namespace FinCashly.Domain.Exceptions;
#nullable disable
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string message, IDictionary<string, string[]> errors = null)
        : base(message)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }
}

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
