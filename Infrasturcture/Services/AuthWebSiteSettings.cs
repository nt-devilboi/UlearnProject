namespace UlearnTodoTimer.Services;

public record AuthWebSiteSettings(
    string ClientId,
    string ClientSecret)
{
    private static readonly string ClientIdEnv = "CLIENT_ID";
    private static readonly string ClientSecretEnv = "CLIENT_SECRET_BOT";
    
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