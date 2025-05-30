using WebApi.Exceptions;

namespace WebApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(context, ex.ErrorCode, ex.Message, ex.StatusCode, ex.Errors);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, "UNEXPECTED_ERROR", ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string errorCode, string message, int statusCode, IReadOnlyList<string>? errors = null)
        {
            var problemDetails = new CustomProblemDetails
            {
                Title = $"{errorCode} OCCURRED",
                Status = statusCode,
                Detail = message,
                ErrorCode = errorCode,
                Errors = (errors == null || !errors.Any()) ? new List<string> { message } : errors.ToList()
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
