namespace UdvBackend.Infrastructure.Result;

public class Result<T,TErr> 
{
    public T? Value { get; }
    public TErr? Error { get; }
    public string? ErrorExplain { get;  }
    private bool _hasError;

    public Result(T value)
    {
        Value = value;
    }

    public Result(TErr error, string? errorExplain = null)
    {
        _hasError = true;
        Error = error;
        ErrorExplain = errorExplain;
    }
    
    public bool HasError() => _hasError;
}