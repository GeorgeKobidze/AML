using System.Net;
using System.Text;

namespace AML.Api.Infrastructure
{
    public class ErrorHandlingMiddleware
    {
        readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            if (exception is UnauthorizedAccessException)
                code = HttpStatusCode.Unauthorized;
            if (exception is Exception)
            {
                code = HttpStatusCode.BadRequest;
            }
            else
            {
                var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
                var request = await FormatRequest(context.Request);
                logger.LogError($" {url} {Environment.NewLine}" +
                    $" {context.Request.HttpContext.Connection.RemoteIpAddress} {Environment.NewLine} {request} " +
                    $"{Environment.NewLine} {exception.ToString()}");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(result);
        }

        private static async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}
