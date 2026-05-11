namespace Gym.API.Middleware;

public sealed class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }

        // =========================
        // Not Found
        // =========================
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status404NotFound,
                ex.Message);
        }

        // =========================
        // Bad Request
        // =========================
        catch (BadRequestException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }

        // =========================
        // Business Rule
        // =========================
        catch (BusinessRuleException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }

        // =========================
        // Conflict
        // =========================
        catch (ConflictException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status409Conflict,
                ex.Message);
        }

        // =========================
        // Database
        // =========================
        catch (DatabaseException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                ex.Message);
        }

        // =========================
        // Unknown Exception
        // =========================
        catch (Exception)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.");
        }
    }

    // =========================
    // Shared Handler
    // =========================

    private static async Task HandleExceptionAsync(
        HttpContext context,
        int statusCode,
        string message,
        object? errors = null)
    {
        context.Response.StatusCode = statusCode;

        var response = ApiResponse<object>.FailureResponse(
            message,
            errors,
            statusCode);

        await context.Response.WriteAsJsonAsync(response);
    }
}