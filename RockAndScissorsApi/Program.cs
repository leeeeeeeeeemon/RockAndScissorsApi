using Microsoft.EntityFrameworkCore;
using RockAndScissorsApi.Contex;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;
// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("GameContext");

builder.Services.AddDbContext<GameContext>(options =>
        options.UseSqlite(connectionString));

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
