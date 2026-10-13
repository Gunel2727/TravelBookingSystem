using System.Text.Json;
using TBS.Application.Common;
using System.Linq;
namespace TBS.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                await WriteResponseAsync(context, 404, ex.Message);
            }
            catch (BadRequestException ex)
            {

                _logger.LogWarning(ex.Message);
                await WriteResponseAsync(context, 400, ex.Message);
            }
            catch (ConflictException ex)
            {

                _logger.LogWarning(ex.Message);
                await WriteResponseAsync(context, 409, ex.Message);
            }
            catch (UnauthorizedException ex)
            {

                _logger.LogWarning(ex.Message);
                await WriteResponseAsync(context, 401, ex.Message);
            }
            catch (ForbiddenException ex)
            {

                _logger.LogWarning(ex.Message);
                await WriteResponseAsync(context, 403, ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                await WriteResponseAsync(context, 400, ex.Errors);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Gözlənilməyən xəta baş verdi");
                await WriteResponseAsync(context, 500, "Server xətası baş verdi");
            }



        }
        private static async Task WriteResponseAsync(
                HttpContext context,
                 int statusCode,
                 string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = ResponseModel<object>.Fail(statusCode, message);


            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
        private static async Task WriteResponseAsync(
                HttpContext context,
                 int statusCode,
                 IDictionary<string, string[]> errors)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var response = ResponseModel<object>.Fail(statusCode, errors.SelectMany(e => e.Value).ToList());
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);
        }
    }
}
