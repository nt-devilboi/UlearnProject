using EduControl;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.MiddleWare;

public class MIddleWareCheckTokenHeader
{
    
    private static readonly ApiResult<object> AccessTokenNotPresentError = new("auth:access-token-not-present", "insert an access_token as bearer in 'Authorization' header", 403);
    private static readonly ApiResult<string> AccessTokenLooksNotABearerError = new("auth:access-token-is-not-bearer", "Authorization header should have format: 'bearer <your token here>'", 403);
    private static readonly ApiResult<object> AccessTokenInvalidError = new("auth:access-token-invalid", "get new access token", 403);
   private static readonly string AuthorizationHeaderPrefix = "bearer";

    private readonly RequestDelegate next;
    private readonly ITokenAccountLinkRepository _tokensAccountLink;
    private readonly ILog log;

    public MIddleWareCheckTokenHeader(RequestDelegate next, ILog log, ITokenAccountLinkRepository tokensAccountLink)
    {
        this.next = next;
        _tokensAccountLink = tokensAccountLink;
        this.log = log.ForContext("AccessTokenMiddleware");
    }

    public async Task InvokeAsync(HttpContext context, TokenScope tokenScope)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authorization) || authorization.Count == 0)
        {
            log.Warn("tried to access api without an access token");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(AccessTokenNotPresentError);
            return;
        }

        var bearer = authorization[0];
        if (bearer.IndexOf(AuthorizationHeaderPrefix, StringComparison.InvariantCultureIgnoreCase) != 0)
        {
            log.Warn("tried to access api with malformed token");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(AccessTokenLooksNotABearerError);
            return;
        }

        var token = bearer.AsSpan()
            .Slice(AuthorizationHeaderPrefix.Length)
            .Trim(' ')
            .ToString();

        var accessToken = await _tokensAccountLink.Get(token);
        
        if (accessToken == null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(AccessTokenInvalidError);
            return;
        }
        
        tokenScope.Token = accessToken;
        await next(context);
    }
}