using ArivalBankServices.API.Validation;
using FluentValidation;

namespace ArivalBankServices.API.Middlewares
{
    internal class ValidationExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                ValidationErrorDetails details =
                    new ValidationErrorDetails(title: "Validation Errors", errors: exception.Errors);

                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }

    public static class ValidationExceptionHandlingInjector
    {
        internal static IApplicationBuilder UseValidationExceptionHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionHandlingMiddleware>();
        }
    }
}
