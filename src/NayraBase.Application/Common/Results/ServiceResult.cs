namespace NayraBase.Application.Common.Results;

/// <summary>
/// Resultado genérico de operaciones de servicio
/// </summary>
public class ServiceResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ServiceResult<T> SuccessResult(T data, string? message = null)
    {
        return new ServiceResult<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static ServiceResult<T> FailureResult(string message, List<string>? errors = null)
    {
        return new ServiceResult<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }

    public static ServiceResult<T> FailureResult(List<string> errors)
    {
        return new ServiceResult<T>
        {
            Success = false,
            Errors = errors
        };
    }
}

/// <summary>
/// Resultado sin datos
/// </summary>
public class ServiceResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ServiceResult SuccessResult(string? message = null)
    {
        return new ServiceResult
        {
            Success = true,
            Message = message
        };
    }

    public static ServiceResult FailureResult(string message, List<string>? errors = null)
    {
        return new ServiceResult
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }

    public static ServiceResult FailureResult(List<string> errors)
    {
        return new ServiceResult
        {
            Success = false,
            Errors = errors
        };
    }
}