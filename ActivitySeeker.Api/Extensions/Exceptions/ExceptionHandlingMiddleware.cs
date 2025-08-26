using FluentValidation;
using Newtonsoft.Json;
using UseCases.Common;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error");

                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var problemDetails = new ValidationProblemDetails(errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.Request.Path
                };

                await WriteProblemDetailsAsync(context, problemDetails, StatusCodes.Status400BadRequest);
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogWarning(ex, "Not found error");
                await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request");
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Internal server error", ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message, Exception? ex = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = message,
                Detail = _env.IsDevelopment() && ex != null ? ex.StackTrace : null,
                Instance = context.Request.Path,
                
            };
            await WriteProblemDetailsAsync(context, problemDetails, statusCode);
        }
        
        private async Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails details, int statusCode)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var json = JsonConvert.SerializeObject(details, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = _env.IsDevelopment() ? Formatting.Indented : Formatting.None,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });

            await context.Response.WriteAsync(json);
        }
    }
}
