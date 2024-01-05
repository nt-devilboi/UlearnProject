namespace UlearnTodoTimer.Services;

public record AuthWebSiteSettings(
    string ClientSecret)
{
    private static readonly string ClientSecretEnv = "CLIENT_SECRET_BOT";
    
    public static AuthWebSiteSettings FromEnv()
    {
        var clientSecret = GetEnvVariable(ClientSecretEnv);
        
        return new AuthWebSiteSettings(clientSecret);
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