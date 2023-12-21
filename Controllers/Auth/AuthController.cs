using System.Net;
using Microsoft.AspNetCore.Mvc;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.Infrasturcture.Services.AppAuth;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers.Auth;

[ApiController]
[Route("oauth/bot")]
public class AuthController : ControllerBase
{
    private readonly ILog _log;
    private readonly ITokenAccountLinkRepository _tokenAccountLinkRepository;
    private readonly IProvideOAuth _provideOAuth;

    public AuthController(
        ILog log,
        ITokenAccountLinkRepository tokenAccountLinkRepository, IProvideOAuth provideOAuth)
    {
        _log = log;
        _tokenAccountLinkRepository = tokenAccountLinkRepository;
        _provideOAuth = provideOAuth;
    }


    [HttpGet]
    public async Task<IActionResult> Auth([FromQuery] string code, [FromQuery(Name = "state")] string oAuthName)
    {
        var requests = _provideOAuth.GetOAuth(oAuthName);
        var accessTokenResponse = await GetAccessToken(requests.CreateGetAccessTokenRequest(code));
        _log.Info($"token {accessTokenResponse}");

        if (accessTokenResponse == null) return NotFound("token not receive");

        await _tokenAccountLinkRepository.Add(accessTokenResponse);

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


    private async Task<AccessTokenResponse?> GetAccessToken(string getAccessTokenRequest)
    {
        var responseAuth = await new HttpClient().GetAsync(getAccessTokenRequest);

        if (responseAuth.StatusCode != HttpStatusCode.OK) return null;

        return await responseAuth.Content.JsonDeserialize<AccessTokenResponse>();
    }
}