using Microsoft.EntityFrameworkCore;
using RockAndScissorsApi.Contex;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;
// Add services to the container.

builder.Services.AddControllers();

string configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile(configPath, optional: false)
        .Build();
Configuration = config;
builder.Services.AddSingleton(config);
builder.Services.AddDbContext<GameContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("GameContext")));

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



app.UseAuthorization();

app.MapControllers();

app.Run();
