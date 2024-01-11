using Newtonsoft.Json;

namespace TgBot.controller.model;

public interface IAccessToken
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}