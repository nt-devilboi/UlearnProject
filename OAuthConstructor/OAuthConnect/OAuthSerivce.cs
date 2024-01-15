using System.Net;
using System.Net.Http.Headers;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.OAuthConstructor.Interfaces;

namespace UlearnTodoTimer.OAuthConstructor;

public class OAuthService : IOAuthService
{
    private readonly IProvideOAuth _provideOAuth;
    public OAuthService(IProvideOAuth provideOAuth)
    {
        _provideOAuth = provideOAuth;
    }

    public async Task<string?> GetAccessToken(string state, string code)
    {
        var nameOAuth = state.Split(":")[0]; //todo сделать реализацию более конкретную.
        var request = _provideOAuth.GetOAuth(nameOAuth).CreateGetAccessTokenRequest(code);
        
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var oAuthResponse =  await client.PostAsync(request, null);
        if (oAuthResponse.StatusCode != HttpStatusCode.OK) return null;
        var token = await oAuthResponse.Content.JsonDeserializeAccessToken();
        
        return token ?? null;
    }

    public  List<string> CreateOAuthRequests(string state = "")
    {
        var oauthRequestsArray = _provideOAuth.GetAll;
        var requestsAuth = new List<string>();

        foreach (var oAuth in oauthRequestsArray)
        {
            requestsAuth.Add(oAuth.Value.CreateAuthRequest($"{oAuth.Key}:{state}"));
        }

        return requestsAuth;
    }
}