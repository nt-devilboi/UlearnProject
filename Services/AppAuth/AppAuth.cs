using System.Net;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.Repositories;
using UlearnTodoTimer.Services.AppAuth;

namespace MyBotTg.Bot;

public class AppAuth: IAppAuth
{
    private readonly IOauthRequests _settings;
    
    public AppAuth(IOauthRequests settings)
    {
        _settings = settings;
    }
    
    public async Task<AccessTokenResponse?> GetAccessToken(string code)
    {
        var requestAuth = _settings.CreateGetAccessTokenRequest(code);
        var responseAuth = await new HttpClient().GetAsync(requestAuth);
        
        if (responseAuth.StatusCode != HttpStatusCode.OK) return null;
        
        return await responseAuth.Content.JsonDeserialize<AccessTokenResponse>();;
    }
}