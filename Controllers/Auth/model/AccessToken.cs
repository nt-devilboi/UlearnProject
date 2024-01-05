namespace TgBot.controller.model;

public class AccessToken
{
    public string Token { get; set; }

    public static implicit operator AccessToken(AccessTokenResponse accessTokenResponse)
        => new AccessToken() { Token = accessTokenResponse.AccessToken };
}