namespace Fs.Models;

public static class ApiResponse
{
    public static ApiResponse<object> Success => new ()
    {
        Success = true
    };
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }

    public ApiResponse(T result)
    {
        Result = result;
        Success = true;
    }

    public ApiResponse()
    {
        
    }
}