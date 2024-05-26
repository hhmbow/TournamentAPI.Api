using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentAPI.Api.Data.Data;
using TournamentAPI.Api.Extensions;
using TournamentAPI.Core.TournamentAPI.Core.Repositories;
using TournamentAPI.Data.TournamentAPI.Data.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TournamentAPIApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAPIApiContext") ?? throw new InvalidOperationException("Connection string 'TournamentAPIApiContext' not found.")));

// Add services to the container.
builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Seed data
app.SeedDataAsync().Wait();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
