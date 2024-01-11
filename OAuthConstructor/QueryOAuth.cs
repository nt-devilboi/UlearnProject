namespace UlearnTodoTimer.OAuthConstructor;

public class QueryOAuth
{
    public string QueryName { get; set; }
    public string Value { get; set; }
    public QueryUse Type { get; set; }

    public QueryOAuth(string queryName,string value , QueryUse type)
    {
        QueryName = queryName;
        Type = type;
        Value = value;
    }
    
    
}