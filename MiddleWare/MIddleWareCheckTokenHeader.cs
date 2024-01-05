using EduControl;
using Microsoft.Win32.SafeHandles;
using TgBot.controller.model;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.Infrasturcture;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.MiddleWare;

public class MiddleWareCheckTokenSesion
{
    private static readonly ApiResult<object> AccessTokenNotPresentError = new("auth:access-token-not-present",
        "you not authorization", 403);

    private static readonly ApiResult<object> AccessTokenInvalidError =
        new("auth:access-token-invalid", "get new access token", 403);

    private readonly RequestDelegate next;
    private readonly ITokenAccountLinkRepository _tokensAccountLink;
    private readonly ILog log;

    public MiddleWareCheckTokenSesion(RequestDelegate next, ILog log, ITokenAccountLinkRepository tokensAccountLink)
    {
        this.next = next;
        _tokensAccountLink = tokensAccountLink;
        this.log = log.ForContext("AccessTokenMiddleware");
    }

    public async Task InvokeAsync(HttpContext context, UserInfoScope userInfoScope)
    {
        var token = context.Session.GetString("token");
        if (token == null)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(AccessTokenNotPresentError);
            return;
        }

        var accessToken = await _tokensAccountLink.Get(token);

        if (accessToken == null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(AccessTokenInvalidError);
            return;
        }


        userInfoScope.Token = new TokenInfo() { Value = accessToken.Value, id = accessToken.Id };
        userInfoScope.UserInfo = new UserInfo()
        {
            Name = "VK Мудак",
            LastName = "не даёт взять данные",
            SecondName =
                "поэтому нужно будет делать доп данные в таблицы, либо пока забить на пользователей в приложений"
        };

        await next(context);
    }
}