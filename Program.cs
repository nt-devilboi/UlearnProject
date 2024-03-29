using System.Reflection;
using EasyOAuth2._0.OAuthConstructor.Extentions;

using UlearnTodoTimer.Application;
using UlearnTodoTimer.Controllers.Model;
using UlearnTodoTimer.MiddleWare;
using UlearnTodoTimer.Repositories;
using UlearnTodoTimer.Services;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;
using UlearnTodoTimer.FluetApi.ConstructorOauth;
using UlearnTodoTimer.OAuthConstructor;
using Microsoft.AspNetCore.CookiePolicy;
using EduControl.MiddleWare;

var builder = WebApplication.CreateBuilder(args);
var oAuth = OAuths.CreateBuilder();

//просто доп инфа: "vk" будет перемещаться в state по ниму мы будет понимать какой сейчас использовать OAuth) 
oAuth.AddVk(_ =>
{
    _.SetRedirectUrl("http://localhost:5128/OAuth/Bot")
        .SetClientId("51749903")
        .SetClientSecret(AuthWebSiteSettings.FromEnv().ClientSecret)
        .SetScope("friends");
});

oAuth.AddOAuth("GitHub", _ =>
{
    _.SetUriPageAuth("login/oauth/authorize")
        .SetUriGetAccessToken("login/oauth/access_token")
        .SetHostServiceOAuth("https://github.com")
        .ConfigureApp()
        .SetRedirectUrl("http://localhost:5128/OAuth/Bot")
        .SetClientSecret("cce83e71b1bcba85fa5493c74fca25e93ec1fb3b")
        .SetClientId("08f51cb49cd389a89b6f");
});

oAuth.AddOAuth("Google", _ =>
{
    _
        .SetHostServiceOAuth("https://accounts.google.com")
        .SetUriPageAuth("o/oauth2/v2/auth")
        .SetResponseType("code")
        .SetUriGetAccessToken("token")
    .ConfigureApp()
        .SetRedirectUrl("http://localhost:5128/OAuth/Bot")
        .SetClientId("")
        .SetClientSecret("")
        .SetScope("https://www.googleapis.com/auth/userinfo.profile");
});

var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true,
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
            policy.AllowAnyHeader();
            policy.AllowCredentials();
            policy.AllowAnyMethod();
            policy.SetIsOriginAllowed(hostName => true);
        });
});


builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()).AddRequestPreProcessor<AddTodoHanlerPre>());
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

app.UseCookiePolicy(new CookiePolicyOptions()
{
    HttpOnly = HttpOnlyPolicy.None,
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseSession();
app.UseAuthorization();
app.UseAuthentication();


app.UseWhen(c => c.Request.Path.StartsWithSegments("/api"),
    c =>
    {
        c.UseMiddleware<MIddleWareCheckTokenHeader>();
        c.UseCors(MyAllowSpecificOrigins);
    });

app.MapControllers();

app.Run();