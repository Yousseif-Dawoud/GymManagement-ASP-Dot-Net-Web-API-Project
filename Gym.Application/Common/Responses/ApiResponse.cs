namespace Gym.Application.Common.Responses;

public class ApiResponse<T>
{
    // Properties with init-only setters to ensure immutability after object creation
    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public T? Data { get; init; }

    public object? Errors { get; init; }

    public int StatusCode { get; init; }

    // Static factory method to create a Success Response
    public static ApiResponse<T> SuccessResponse(T data,string message = "Request completed successfully.",int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = statusCode
        };
    }

    // Static factory method to create an Failure Response
    public static ApiResponse<T> FailureResponse(string message,object? errors = null,int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}