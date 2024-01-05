namespace UlearnTodoTimer.Infrasturcture.Services.AppAuth;

public class OAuthData
{
    public string RedirectUrl = "http://localhost:5128/OAuth/Bot";
    public string ResponseType = "code";
    public string? Version = "5.131";
    public string Scope = "friends";
    public string ClientSecret;
    public string ClientId;
    public string ServiceOAuth = "https://oauth.vk.com";
    public string UriAuthorization = "authorize";
    public string UriGetAccessToken = "access_token";
    public string Display = "page";
}