namespace UlearnTodoTimer.OAuthConstructor
{
    public class OAuthGoogle
    {
        public string RedirectUrl = "http://localhost:5128/OAuth/Bot";
        public string ResponseType = "code";
        public string? Version = "5.131";
        public string Scope = "userinfo.email";
        public string ClientSecret;
        public string ClientId;
        public string ServiceOAuth = "https://www.googleapis.com";
        public string UriAuthorization = "auth";
        public string UriGetAccessToken = "access_token";
        public string Display = "page";
    }
}
