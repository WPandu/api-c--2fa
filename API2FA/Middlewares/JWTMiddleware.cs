namespace API2FA.Middlewares
{
    using API2FA.IServices;
    using API2FA.Helpers;

    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, TokenManager tokenManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null) {
                throw new System.UnauthorizedAccessException("Unauthorized");
            }

            var userID = tokenManager.ValidateToken(token);
            if (userID != null)
            {
                // attach user to context on successful jwt validation
                context.Items["UserID"] = userID;
            }

            await _next(context);
        }
    }
}
