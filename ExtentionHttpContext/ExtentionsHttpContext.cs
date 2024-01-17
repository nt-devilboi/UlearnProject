using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TgBot.controller.model;

namespace TgBot.ExtentionHttpContext;

public static class ExtensionsttpContext
{
    public static async Task<T?> JsonDeserialize<T>(this HttpContent httpContent) where T : IAccessToken
    {
        var dataJson = await httpContent.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(dataJson, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore
        });
    }


    public static async Task<string?> JsonDeserializeAccessToken(this HttpContent httpContent)
    {
        var dataJson = await httpContent.ReadAsStringAsync();
        
        var x = JsonDocument.Parse(dataJson).RootElement;
        var token = x.GetProperty("access_token").GetString();

        return token;
    }
}