using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TgBot.controller.model;

public record AccessTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    [JsonProperty("expires_in")]    
    public string ExpiresIn { get; set; }
    [JsonProperty("user_id")]
    public string UserId { get; set; }
}