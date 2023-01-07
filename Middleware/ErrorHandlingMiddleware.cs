using MKFotografiaBackend.Exceptions;

namespace MKFotografiaBackend.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        //TODO: Add ILogger
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbiddenException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Odmowa dostępu");
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                //TODO: _logger.LogError(e, e.Message);
                Console.Write(e.ToString());
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Niespodziewany błąd.");
            }
        }
    }
}
