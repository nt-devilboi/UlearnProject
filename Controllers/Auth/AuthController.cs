using System.Net;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.Application;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.OAuthConstructor;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers.Auth;

[ApiController]
[Route("OAuth/Bot")]
public class AuthController : Controller
{
    private readonly ILog _log;
    private readonly ITokenAccountLinkRepository _tokenAccountLinkRepository;
    private readonly IOAuthService _ioAuthService;

    public AuthController(
        ILog log,
        ITokenAccountLinkRepository tokenAccountLinkRepository,
        IOAuthService ioAuthService)
    {
        _log = log;
        _tokenAccountLinkRepository = tokenAccountLinkRepository;
        _ioAuthService = ioAuthService;
    }

    //привет принцип OCP. класс закрыт для изменений и открыт для расширения.
    [HttpGet]
    public async Task<IActionResult> Auth([FromQuery] string code, [FromQuery(Name = "state")] string state)
    {
        _log.Info("авторизация проходит");
        var token = await _ioAuthService.GetAccessToken(state, code);

        if (token == null) return NotFound("token not receive");

        _log.Info($"token {token}");

        var tokenEntity = Token.From(token, state);
        await _tokenAccountLinkRepository.Add(tokenEntity);
        HttpContext.Response.Cookies.Append("token", tokenEntity.Value);

        return Redirect("https://localhost:5173");
    }

    [HttpGet("get/oauth/requests")]
    public ActionResult<List<string>> GetRequest()
    {
        return _ioAuthService.CreateOAuthRequests();
    }
}