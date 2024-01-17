namespace UlearnTodoTimer.OAuthConstructor.Interfaces;


public interface IProvideOAuth
{
    public IOauthRequests GetOAuth(string name);

    public IReadOnlyDictionary<string, IOauthRequests> GetAll { get; }
    
    
}