using System.Net;
using MediatR;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;

namespace UlearnTodoTimer.Application;

public class HandlerGetTokenOAuth : IRequestHandler<GetTokenRequest, GetTokenResponse>
{
    public async Task<GetTokenResponse> Handle(GetTokenRequest request, CancellationToken cancellationToken)
    {
        var client = new HttpClient();
        var oAuthResponse =  await client.GetAsync(request.AccessTokenRequest, cancellationToken);
        if (oAuthResponse.StatusCode != HttpStatusCode.OK) return new GetTokenResponse(null);

        var accessToken = await oAuthResponse.Content.JsonDeserialize<AccessTokenResponse>();
        if (accessToken == null) return new GetTokenResponse(null);
        
        return new GetTokenResponse(accessToken);
    }
}

public record GetTokenRequest(string AccessTokenRequest) : IRequest<GetTokenResponse>;

public record GetTokenResponse(AccessTokenResponse? AccessTokenResponse);