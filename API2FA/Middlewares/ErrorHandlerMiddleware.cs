namespace API2FA.Middlewares
{
    using System.Net;
    using System.Text.Json;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case UnauthorizedAccessException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case MissingFieldException:
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
