namespace API2FA.Middlewares
{
    using API2FA.IServices;
    using API2FA.Helpers;
    using Microsoft.Extensions.Primitives;
    using API2FA.Services;

    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, TokenManager tokenManager)
        {
            var path = context.Request.Path.Value;
            if (path == "/auth/login")
            {
                await _next(context);
                return;
            }
            context.Request.Headers.TryGetValue("Authorization", out StringValues authorizationHeader);
            var token = authorizationHeader.ToString().Split(" ").Last();
            if (token == null) {
                throw new System.UnauthorizedAccessException("Unauthorized");
            }

            var userID = tokenManager.ValidateToken(token);
            if (userID == null)
            {
                throw new System.UnauthorizedAccessException("Unauthorized");
            }

            context.Items["User"] = userService.GetById(new Guid(userID));
            await _next(context);
        }
    }
}
