using UlearnTodoTimer.Infrasturcture.Services.AppAuth;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.OAuthConstructor.Requests;

namespace UlearnTodoTimer.FluetApi.ConstructorOauth;

public class ConstructorOAuth
{
    private static readonly string ClientSecretEnv = "CLIENT_SECRET_BOT";
    private OAuthData _oAuthData = new OAuthData();

    public ConstructorOAuth SetRedirectUrl(string redirectUrl)
    {
        _oAuthData.RedirectUrl = redirectUrl;

        return this;
    }
    public ConstructorOAuth SetClientSecret(string clientSecret)
    {
        _oAuthData.ClientSecret = clientSecret;

        return this;
    }

    public ConstructorOAuth SetDisplay(string display)
    {
        _oAuthData.Display = display;

        return this;
    }
    public ConstructorOAuth SetClientId(string clientId)
    {
        _oAuthData.ClientId = clientId;

        return this;
    }
    
    public ConstructorOAuth SetVersion(string v)
    {
        _oAuthData.Version = v;

        return this;
    }

    public ConstructorOAuth SetHostServiceOAuth(string hostServiceOAuth)
    {
        _oAuthData.ServiceOAuth = hostServiceOAuth;

        return this;
    }

    public ConstructorOAuth SetUriAuth(string uriAuth)
    {
        _oAuthData.UriAuthorization = uriAuth;

        return this;
    }
    
    public ConstructorOAuth SetUriGetAccessToken(string uriGetToken)
    {
        _oAuthData.UriGetAccessToken = uriGetToken;

        return this;
    }
    
    public ConstructorOAuth SetScope(string scope)
    {
        _oAuthData.Scope = scope;

        return this;
    }
    
    public ConstructorOAuth SetResponseType(string type)
    {
        _oAuthData.ResponseType = type;

        return this;
    }
    
    
    public IOauthRequests Build()
    {
        var oAuth =  new OAuthRequests(_oAuthData);

        return oAuth;
    }
    
}