using Microsoft.EntityFrameworkCore;
using RestSharp;
using WebHookService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WebhookContext>(options => options.UseSqlServer("Data Source=(local);Initial Catalog=WebhookDb;Integrated Security=True;TrustServerCertificate=True"));
builder.Services.AddScoped<RestClient>();
builder.Services.AddScoped<WebhookService>(sp =>
{
	var ctx = sp.GetService<WebhookContext>();
	var client = sp.GetService<RestClient>();
	return new WebhookService(ctx, client);
});

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
