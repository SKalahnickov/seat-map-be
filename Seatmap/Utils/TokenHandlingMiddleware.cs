namespace Seatmap.Utils
{
    public class TokenHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenHolder tokenHolder)
        {
            var token = context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(token))
                tokenHolder.AuthToken = token;
            await _next(context);
        }
    }
}
