using O2morny.Application.Common.Exceptions;
using FluentValidation;

namespace O2morny.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppValidationException ex)
            {
                await HandleExceptionAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    new
                    {
                        message = "Validation failed",
                        errors = ex.Errors
                    });
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(
                    context,
                    StatusCodes.Status404NotFound,
                    ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(
                    context,
                    StatusCodes.Status401Unauthorized,
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(
                    context,
                    StatusCodes.Status500InternalServerError,
                    "Something went wrong.");
            }
        }


        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, object response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
