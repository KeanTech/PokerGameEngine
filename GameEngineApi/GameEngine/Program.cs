using GameEngine.Core.Enums;
using GameEngine.Core.Services.Webhook;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GameEngine.Data;
using GameEngine.Core.Managers;
using GameEngine.Models.Game;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameEngineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameEngineContext") ?? throw new InvalidOperationException("Connection string 'GameEngineContext' not found.")));
builder.Services.AddScoped<IWebhookService, WebhookService>();


// Add services to the container.
builder.Services.AddScoped<IGameManager, GameManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var sp = app.Services.CreateScope())
{
	var ctx = sp.ServiceProvider.GetService<GameEngineContext>();
	ctx?.Database.EnsureCreated();
	if (ctx.Card.IsNullOrEmpty())
	{
		var cards = new List<Card>();
		foreach (Symbols symbol in Enum.GetValues(typeof(Symbols)))
		{
			foreach (CardTypes ct in Enum.GetValues(typeof(CardTypes)))
			{
				cards.Add(new Card() { Symbol = symbol, Type = ct });
			}
		}
		ctx?.Card.AddRange(cards);
		ctx?.SaveChanges();
	}
}

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