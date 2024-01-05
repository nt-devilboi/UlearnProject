using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using UlearnTodoTimer.Application;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.FluetApi.ConstructorOauth;
using UlearnTodoTimer.MiddleWare;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.Repositories;
using UlearnTodoTimer.Services;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);
var oAuth = OAuths.CreateBuilder();

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

/*oAuth.AddOAuth("google", _ =>
{
    _.SetRedirectUrl("http://localhost:5128/OAuth/Bot")
    .SetScope("")
    .SetHostServiceOAuth("https://www.googleapis.com")
    .SetUriAuth("auth")
});*/
// Add services to the container.

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


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.WebHost.UseUrls("http://localhost:5128"); // оо великий костыль

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITokenAccountLinkRepository, TokenAccountLinkRepository>();

builder.Services.AddSingleton<IProvideOAuth>(_ => (IProvideOAuth)oAuth);
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