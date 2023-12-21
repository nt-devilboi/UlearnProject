namespace UlearnTodoTimer.Services;

public record AuthWebSiteSettings(
    string ClientId,
    string ClientSecret)
{
    private static readonly string ClientIdEnv = "CLIENT_ID";
    private static readonly string ClientSecretEnv = "CLIENT_SECRET_BOT";

    /*
    private const string RedirectUrl = "http://localhost:5128/OAuth/Bot";
    private const string ResponseType = "code";
    private const string Version = "5.131";
    public const string Scope = "friends";
    */

    /*public string CreateAuthRequest(string state)
        => "https://oauth.vk.com/authorize?" +
           $"client_id={ClientId}&state={state}&" +
           "display=page&" +
           $"scope={Scope}&" +
           $"redirect_uri={RedirectUrl}&" +
           $"response_type={ResponseType}&" +
           $"v={Version}";
    
    public string CreateGetAccessTokenRequest(string code)
    {
        return $"https://oauth.vk.com/access_token?" +
               $"client_id={ClientId}&" +
               $"client_secret={ClientSecret}&" +
               $"redirect_uri={RedirectUrl}&" +
               $"code={code}";
    }*/
    
    public static AuthWebSiteSettings FromEnv()
    {
        var clientSecret = GetEnvVariable(ClientSecretEnv);
        var clientId = GetEnvVariable(ClientIdEnv);
        
        return new AuthWebSiteSettings(clientId, clientSecret);
    }
    
    private static string GetEnvVariable(string name)
    {
        var value = Environment.GetEnvironmentVariable(name)
                    ?? Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);

        if (value == null)
            throw new InvalidOperationException($"env variable '{name}' not found");
        return value;
    }
}