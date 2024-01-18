using UlearnTodoTimer.OAuthConstructor.Interfaces;

namespace UlearnTodoTimer.OAuthConstructor.Extentions;

public static class AddOAuthExtenions
{
    /*public static AddVk(this IRegisterOAuth oAuth)
    {
        re
    }*/

    public static IServiceCollection AddOAuths(this IServiceCollection services, IRegisterOAuth OAuth)
    {
        services.AddSingleton<IProvideOAuth>(_ => OAuth as IProvideOAuth ?? throw new Exception("OAuth Not Found"));
        return services;
    }
}