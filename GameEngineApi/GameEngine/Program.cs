using GameEngine.Core.Enums;
using GameEngine.Core.Services.Webhook;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameEngine.Data;
using GameEngine.Core.Managers;
using GameEngine.Models.Game;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameEngineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameEngineContext") ?? throw new InvalidOperationException("Connection string 'GameEngineContext' not found.")));
builder.Services.AddDbContext<WebhookContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("WebHookContext") ??
	                     throw new InvalidOperationException("Connection string 'GameEngineContext' not found.")));
builder.Services.AddScoped<IWebhookService, WebhookService>();


// Add services to the container.
builder.Services.AddScoped<IGameManager, GameManager>();

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


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
