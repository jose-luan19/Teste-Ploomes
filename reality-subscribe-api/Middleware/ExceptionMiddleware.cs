using Application.Exceptions;
using FluentValidation;
using Models.ViewModel;
using System.Net;

namespace reality_subscribe_api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetails = this.GetErrorDetails(context, exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDetails.StatusCode;
            await context.Response.WriteAsync(errorDetails.ToString());
        }

        public ErrorDetails GetErrorDetails(HttpContext context, Exception exception)
        {
            var message = exception.InnerException != null ? exception.InnerException.ToString() : exception.Message;
            var result = exception.GetType().Name switch
            {
                nameof(UnauthorizedAccessException) => new ErrorDetails() { StatusCode = (int)HttpStatusCode.Unauthorized, Message = message },
                nameof(ValidationException) => new ErrorDetails() { StatusCode = (int)HttpStatusCode.UnprocessableEntity, Message = message },
                nameof(InvalidOperationException) => new ErrorDetails() { StatusCode = (int)HttpStatusCode.BadRequest, Message = message },
                nameof(NotFoundException) => new ErrorDetails() { StatusCode = (int)HttpStatusCode.NotFound, Message = message },
                _ => new ErrorDetails() { StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Internal Server Error" },
            };

            _logger.LogError("\n\n {url} \n\n {statusCode} \n\n {message} \n\n", context.Request?.Path.Value, result.StatusCode, result.Message);
            _logger.LogError($"GlobalExceptionFilter: Error in {exception.TargetSite.DeclaringType.FullName}. \n\n {exception.Message}. \n\n\n");

            return result;

        }
    }
}
