using UlearnTodoTimer.FluetApi.ConstructorOauth;
using UlearnTodoTimer.MiddleWare;
using UlearnTodoTimer.OAuthConstructor.Interfaces;
using UlearnTodoTimer.Services;
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
        .SetClientId(AuthWebSiteSettings.FromEnv().ClientId)
        .SetClientSecret(AuthWebSiteSettings.FromEnv().ClientSecret);
}); // todo: можно сделать метот расширение который часть запросов пишет сам: например "AddVkOAuthWebSite"
// Add services to the container.

var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true,
});


builder.Logging.ClearProviders();
builder.Logging.AddVostok(log);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProvideOAuth>(_ => (IProvideOAuth)oAuth);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWhen(c => c.Request.Path.StartsWithSegments("api"),
    c => { c.UseMiddleware<MIddleWareCheckTokenHeader>(); });

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();