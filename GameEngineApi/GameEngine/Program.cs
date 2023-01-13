using GameEngine.Core.Services.Webhook;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameEngine.Data;
using GameEngine.Core.Managers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameEngineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameEngineContext") ?? throw new InvalidOperationException("Connection string 'GameEngineContext' not found.")));
builder.Services.AddDbContext<WebhookContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("WebHookContext") ??
	                     throw new InvalidOperationException("Connection string 'GameEngineContext' not found.")));
builder.Services.AddScoped<IWebhookService, WebhookService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WebHook>();
builder.Services.AddSingleton<IGameManager, GameManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
