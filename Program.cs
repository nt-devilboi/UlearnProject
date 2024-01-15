using System.Reflection;
using UlearnTodoTimer.Application;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.FluetApi.ConstructorOauth;
using UlearnTodoTimer.MiddleWare;
using UlearnTodoTimer.OAuthConstructor;
using UlearnTodoTimer.OAuthConstructor.Extentions;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.Repositories;
using UlearnTodoTimer.Services;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);
var oAuth = UlearnTodoTimer.FluetApi.ConstructorOauth.OAuths.CreateBuilder();

//просто доп инфа: "vk" будет перемещаться в state по ниму мы будет понимать какой сейчас использовать OAuth) 
oAuth.AddOAuth("vk", _ =>
{
    _.SetRedirectUrl("http://localhost:5128/OAuth/Bot")
        .SetScope("friends")
        .SetHostServiceOAuth("https://oauth.vk.com")
        .SetUriAuth("authorize")
        .SetUriGetAccessToken("access_token")
        .SetDisplay("page")
        .SetResponseType("code")
        .SetVersion("5.131")
        .SetClientId("51749903")
        .SetClientSecret(AuthWebSiteSettings.FromEnv().ClientSecret);
}); // todo: можно сделать метот расширение который часть запросов пишет сам: например "AddVkOAuthWebSite"

oAuth.AddOAuth("GitHub", _ =>
{
    _.SetRedirectUrl("http://localhost:5128/OAuth/Bot")
        .SetHostServiceOAuth("https://github.com")
        .SetUriAuth("login/oauth/authorize")
        .SetClientSecret("cce83e71b1bcba85fa5493c74fca25e93ec1fb3b")
        .SetClientId("08f51cb49cd389a89b6f")
        .SetUriGetAccessToken("login/oauth/access_token");
});

oAuth.AddOAuth("Google", _ =>
{
    _.SetRedirectUrl("http://localhost:5128/OAuth/Bot")
    .SetHostServiceOAuth("https://accounts.google.com")
    .SetUriAuth("o/oauth2/v2/auth")
    .SetClientId("")
    .SetClientSecret("")
    .SetScope("https://www.googleapis.com/auth/userinfo.email")
    .SetResponseType("code")
    .SetUriGetAccessToken("https://oauth2.googleapis.com/token");
});

var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true,
});

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()).AddRequestPreProcessor<AddTodoHanlerPre>());
builder.Services.AddSingleton<ITodoRepo, TodoRepoFake>();
builder.Logging.ClearProviders();

builder.Logging.AddVostok(log);
builder.Services.AddSingleton<ILog>(log);
builder.Services.AddControllers();

builder.Services.AddSingleton<IOAuthService, OAuthService>();
builder.Services.AddOAuths(oAuth);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.WebHost.UseUrls("http://localhost:5128"); // оо великий костыль

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITokenAccountLinkRepository, TokenAccountLinkRepository>();

builder.Services.AddScoped<UserInfoScope>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseSession();
app.UseAuthorization();
app.UseAuthentication();


app.UseWhen(c => c.Request.Path.StartsWithSegments("/api"),
    c => { c.UseMiddleware<MiddleWareCheckTokenSesion>(); });

app.MapControllers();

app.Run();