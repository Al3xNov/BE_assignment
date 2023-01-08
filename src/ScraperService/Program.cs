using Common.Entities;
using Common.Interfaces;
using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var client = new MongoClient("mongodb://localhost:27017");
builder.Services.AddControllers();
builder.Services.AddSingleton<IMongoBaseRepository<ExchangeRateDoc>>(new ExchangeRateMongoRepository(client.GetDatabase("Exchanges").GetCollection<ExchangeRateDoc>("Rates")));
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
