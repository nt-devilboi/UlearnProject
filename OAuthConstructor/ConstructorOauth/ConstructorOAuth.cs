using UlearnTodoTimer.OAuthConstructor;
using UlearnTodoTimer.OAuthConstructor.Extentions;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.OAuthConstructor.Requests;

namespace UlearnTodoTimer.FluetApi.ConstructorOauth;

public class ConstructorOAuth
{
    private OAuthData _oAuthData = new OAuthData();
    
    public ConstructorOAuth SetRedirectUrl(string redirectUri)
    {
        _oAuthData.AddQuery(nameof(redirectUri).AsSnakeCase(), redirectUri, QueryUse.All);
        return this;
    }
    public ConstructorOAuth SetClientSecret(string clientSecret)
    {
        _oAuthData.AddQuery(nameof(clientSecret).AsSnakeCase(), clientSecret, QueryUse.OnlyGetAccessToken);
        return this;
    }

    public ConstructorOAuth SetDisplay(string display)
    {
        _oAuthData.AddQuery(nameof(display).AsSnakeCase(), display, QueryUse.OnlyCreateRequest);
        return this;
    }
    public ConstructorOAuth SetClientId(string clientId)
    {
        _oAuthData.AddQuery(nameof(clientId).AsSnakeCase(), clientId, QueryUse.All);
        return this;
    }
    
    public ConstructorOAuth SetVersion(string version)
    {
        _oAuthData.AddQuery(nameof(version).AsSnakeCase(), version, QueryUse.OnlyCreateRequest); // по идей можно вынести часть кода, вот этого
        return this;
    }

    public ConstructorOAuth SetCustomQuery(string nameQuery, string value, QueryUse queryUse)
    {
        _oAuthData.AddQuery(nameQuery, value, queryUse);
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
        _oAuthData.AddQuery(nameof(scope), scope, QueryUse.OnlyCreateRequest);
        return this;
    }
    
    public ConstructorOAuth SetResponseType(string responseType)
    {
        _oAuthData.AddQuery(nameof(responseType).AsSnakeCase(), responseType, QueryUse.OnlyCreateRequest);
        return this;
    }
    
    
    public IOauthRequests Build()
    {
        if (_oAuthData.ServiceOAuth == string.Empty)
        {
            throw new ArgumentException("Not set Service Authorization");
        }

        if (_oAuthData.UriAuthorization == string.Empty)
        {
            throw new ArgumentException("Not Set uri for Authorization");
        }

        if (_oAuthData.UriGetAccessToken == string.Empty)
        {
            throw new ArgumentException("Not set Uri for Get token");
        }

        if (!_oAuthData.Contains("client_id"))
        {
            throw new ArgumentException("Not Set client id");
        }

        if (!_oAuthData.Contains("client_secret"))
        {
            throw new ArgumentException("Not set client secret");
        }
        
        var oAuth =  new OAuthRequests(_oAuthData);
        return oAuth;
    }
    
}