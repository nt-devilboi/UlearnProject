using UlearnTodoTimer.Infrasturcture.Services.AppAuth;
using UlearnTodoTimer.OAuthConstructor.Interfaces;

namespace UlearnTodoTimer.OAuthConstructor.Requests;

public class OAuthRequests : IOauthRequests
{
    private readonly OAuthData OAuthData;
    public string Scope => OAuthData.Scope;

    public OAuthRequests(OAuthData oAuthData)
    {
        OAuthData = oAuthData;
    }

    public string CreateAuthRequest(string state = "")
    {
        var req = $"{OAuthData.ServiceOAuth}/{OAuthData.UriAuthorization}?" +
                  $"client_id={OAuthData.ClientId}&" +
                  TryGetQueryState(state) +
                  TryGetQueryDisplay() +
                  $"scope={OAuthData.Scope}&" +
                  $"redirect_uri={OAuthData.RedirectUrl}&" +
                  $"response_type={OAuthData.ResponseType}&" +
                  TryGetQueryV();
        
        return req;
    }

    public string CreateGetAccessTokenRequest(string code)
    {
        return $"{OAuthData.ServiceOAuth}/{OAuthData.UriGetAccessToken}?" +
               $"client_id={OAuthData.ClientId}&" +
               $"client_secret={OAuthData.ClientSecret}&" +
               $"redirect_uri={OAuthData.RedirectUrl}&" +
               $"code={code}";
    }


    private string TryGetQueryDisplay()
        => OAuthData.Display != string.Empty ? $"display={OAuthData.Display}&" : string.Empty;

    private static string TryGetQueryState(string state)
        => state != string.Empty ? $"state={state}&" : string.Empty;

    private string TryGetQueryV()
        => OAuthData.Version != string.Empty ? $"v={OAuthData.Version}" : string.Empty;
}