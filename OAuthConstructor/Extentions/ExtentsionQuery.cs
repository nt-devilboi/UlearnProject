using System.Text;

namespace UlearnTodoTimer.OAuthConstructor.Extentions;

public static class ExtentsionQuery
{
    public static string AsSnakeCase(this string query)
    {
        var stringBuilder = new StringBuilder();
        foreach (var c in query)
        {
            if (char.IsUpper(c) && stringBuilder.Length != 0) stringBuilder.Append('_');
         
            stringBuilder.Append(char.ToLower(c));
        }
        
        return stringBuilder.ToString();
    }  
    
    public static string AddQueryValue(this string nameQuery, string value)
    {
        return $"{nameQuery}={value}";
    }
}