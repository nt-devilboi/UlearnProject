using System.Net;
using System.Net.Http.Headers;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.OAuthConstructor.Interfaces;

namespace UlearnTodoTimer.OAuthConstructor;

public class OAuthService : IOAuthService
{
    private readonly IProvideOAuth _provideOAuth;
    private readonly IHttpClientFactory _clientFactory;
    public OAuthService(IProvideOAuth provideOAuth, IHttpClientFactory clientFactory)
    {
        _provideOAuth = provideOAuth;
        _clientFactory = clientFactory;
    }

    public async Task<string?> GetAccessToken(string state, string code) 
    {
        var request = _provideOAuth.GetOAuth(state).CreateGetAccessTokenRequest(code);

        var client = _clientFactory.CreateClient();
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