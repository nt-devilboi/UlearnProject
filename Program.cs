using EduControl.MiddleWare;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);

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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWhen(c => c.Request.Path.StartsWithSegments("api"), c =>
{
    c.UseMiddleware<MIddleWareCheckTokenHeader>();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();