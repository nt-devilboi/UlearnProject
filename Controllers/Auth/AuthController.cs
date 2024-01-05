using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.Application;
using UlearnTodoTimer.Domen.Entities;
using UlearnTodoTimer.Infrasturcture.Services.AppAuth;
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
    private readonly IProvideOAuth _provideOAuth;
    private readonly IMediator _mediator;
    public AuthController(
        ILog log,
        ITokenAccountLinkRepository tokenAccountLinkRepository,
        IProvideOAuth provideOAuth,
        IMediator mediator)
    {
        _log = log;
        _tokenAccountLinkRepository = tokenAccountLinkRepository;
        _provideOAuth = provideOAuth;
        _mediator = mediator;
    }

    //привет принцип OCP. класс закрыт для изменений и открыт для расширения.
    [HttpGet]
    public async Task<IActionResult> Auth([FromQuery] string code, [FromQuery(Name = "state")] string oAuthName)
    {
        _log.Info("авторизация проходит");
        var request = _provideOAuth.GetOAuth(oAuthName).CreateGetAccessTokenRequest(code);
        var accessTokenResponse = await _mediator.Send(new GetTokenRequest(request));

        if (accessTokenResponse.AccessTokenResponse == null) return NotFound("token not receive");

        _log.Info($"token {accessTokenResponse.AccessTokenResponse}");

        var token = Token.From(accessTokenResponse.AccessTokenResponse, oAuthName);
        await _tokenAccountLinkRepository.Add(token);
        HttpContext.Session.SetString($"token", token.Value);
        
        return Ok();
    }
    
    [HttpGet("get/oauth/requests")]
    public ActionResult<List<string>> GetRequest()
    {
        var oauthRequestsArray = _provideOAuth.GetAll;
        var requestsAuth = new List<string>();

        foreach (var oAuth in oauthRequestsArray)
        {
            requestsAuth.Add(oAuth.Value.CreateAuthRequest(oAuth.Key));
        }

        return requestsAuth;
    }
}