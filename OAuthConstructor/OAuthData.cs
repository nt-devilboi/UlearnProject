using UlearnTodoTimer.OAuthConstructor.Extentions;

namespace UlearnTodoTimer.OAuthConstructor;

internal class OAuthData // по идей можно сделать internal, если будет в виде либы
{
    public string ServiceOAuth { get; set; } = string.Empty;
    public string UriAuthorization { get; set; } = string.Empty;
    public string UriGetAccessToken { get; set; } = string.Empty;

    private readonly List<QueryOAuth>
        QueryOAuths = new List<QueryOAuth>(); // по идей можно сделать internal, если будет в виде либы

    public bool Contains(string queryName)
    {
        return QueryOAuths.FirstOrDefault(x => x.QueryName == queryName) != null; //todo make more Performance 
    }
    
    public void AddQuery(string queryName, string value, QueryUse queryUse)
    {
        QueryOAuths.Add(new QueryOAuth(queryName, value, queryUse));
    }

    public IEnumerable<string> GetOAuthRequestQueries()
        => QueryOAuths
            .Where(x => x.Type is QueryUse.OnlyCreateRequest or QueryUse.All)
            .Select(x => x.QueryName.AddQueryValue(x.Value));


    public IEnumerable<string> GetAccessTokenQueries()
        => QueryOAuths
            .Where(x => x.Type is QueryUse.OnlyGetAccessToken or QueryUse.All)
            .Select(x => x.QueryName.AddQueryValue(x.Value));
}