using UlearnTodoTimer.OAuthConstructor.Extentions;
using UlearnTodoTimer.OAuthConstructor.Interfaces;

namespace UlearnTodoTimer.OAuthConstructor.Requests;

internal class OAuthRequests : IOauthRequests
{
    private readonly OAuthData _oAuthData;

    public OAuthRequests(OAuthData oAuthData)
    {
        _oAuthData = oAuthData;
    }
    
    //Todo: можно немного порефакторить
    public string CreateAuthRequest(string state)
    {
        return $"{_oAuthData.ServiceOAuth}/{_oAuthData.UriAuthorization}?"
               + "state".AddQueryValue(state) + "&"
               + string.Join("&", _oAuthData.GetOAuthRequestQueries()).TrimEnd('&');
    }

    public string CreateGetAccessTokenRequest(string code)
    {
        return $"{_oAuthData.ServiceOAuth}/{_oAuthData.UriGetAccessToken}?" 
               + "code".AddQueryValue(code) + "&" 
               + string.Join("&", _oAuthData.GetAccessTokenQueries()).TrimEnd('&');
    }
}