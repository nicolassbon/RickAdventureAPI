using Microsoft.EntityFrameworkCore;
using RickAdventureAPI.Data.Entidades;
using RickAdventureAPI.Logica;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext con connection string desde appsettings.json
builder.Services.AddDbContext<RickAdventureGameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RickAdventureDB")));

// Registrar servicios de lógica
builder.Services.AddScoped<IJugadorLogica, JugadorLogica>();
builder.Services.AddScoped<IPartidaLogica, PartidaLogica>();

// CORS para Unity
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUnity", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar JSON para evitar ciclos de referencia
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowUnity");
app.UseAuthorization();
app.MapControllers();

app.Run();
