using EduControl;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Infrasturcture;
using UlearnTodoTimer.Repositories;
using VkNet.Model;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.MiddleWare;

public class MiddleWareCheckTokenCookie
{
    private static readonly ApiResult<string>
        TokenNotFound = new("auth:cookie-is-empty", "try authorization", 403);

    private static readonly ApiResult<string> TokenNotLinkAccount =
        new("auth:token-not-link-account", "you have token, but it haven't account", 403);
    private readonly RequestDelegate _next;
    private readonly ILog _log;
    private readonly ITokenAccountLinkRepository _tokensAccountLink;
    public MiddleWareCheckTokenCookie(
        RequestDelegate next,
        ILog log, ITokenAccountLinkRepository tokensAccountLink)
       {
        _next = next;
        _log = log;
        _tokensAccountLink = tokensAccountLink;
       }

    public async Task InvokeAsync(HttpContext context, UserInfoScope accountScope)
    {
        if (!context.Request.Cookies.TryGetValue("token", out var token) || string.IsNullOrEmpty(token))
        {
            await context.Response.WriteAsJsonAsync(TokenNotFound);
            return;
        }
        
        _log.Info($"token here: {token}");
        var accountResponse = await _tokensAccountLink.Get(token);
        if (accountResponse == null)
        {
            _log.Warn($"Account With token{token} Not Found");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(accountResponse);
            return;
        }
        
        if (token == null)
        {
            _log.Warn($"Account With token{token} Not Found");
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(TokenNotFound);
            return;
        }
        
        accountScope.Token = new TokenInfo() { Value = accountResponse.Value, id = accountResponse.Id};
        await _next(context);
    }
}