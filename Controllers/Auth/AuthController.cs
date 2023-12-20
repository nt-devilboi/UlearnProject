using Microsoft.AspNetCore.Mvc;
using UlearnTodoTimer.Repositories;
using Vostok.Logging.Abstractions;

namespace UlearnTodoTimer.Controllers.Auth;

[ApiController]
[Route("oauth/bot")]
public class AuthController : ControllerBase
{
    private readonly ILog _log;
    private readonly IAppAuth _appAuth;
    private readonly ITokenAccountVkRepository _tokenAccountVkRepository;

    public AuthController(
        IAppAuth appAuth,
        ILog log,
        ITokenAccountVkRepository tokenAccountVkRepository
    )
    {
        _appAuth = appAuth;
        _log = log;
        _tokenAccountVkRepository = tokenAccountVkRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Auth([FromQuery] string code, [FromQuery(Name = "state")] string chatId)
    {
        var accessTokenResponse = await _appAuth.GetAccessToken(code);
        _log.Info($"token {accessTokenResponse}");

        if (accessTokenResponse == null) return NotFound("token not receive");

        await _tokenAccountVkRepository.Add(accessTokenResponse, long.Parse(chatId));

        return Ok();
    }
}