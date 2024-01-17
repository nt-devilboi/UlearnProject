using UlearnTodoTimer.OAuthConstructor.Extentions;

namespace UlearnTodoTimer.OAuthConstructor;

internal class OAuthData // по идей можно сделать internal, если будет в виде либы
{
    public string RedirectUrl;
    public string ResponseType;
    public string? Version;
    public string Scope;
    public string ClientSecret;
    public string ClientId;
    public string ServiceOAuth;
    public string UriAuthorization;
    public string UriGetAccessToken ;
    public string Display;
}