using backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodanie dostêpu do bazy PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseNpgsql(connectionString));

// Konfiguracja CORS (zezwalamy na dostêp z frontendu)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:8080")  // Zezwalamy na dostêp z frontendowego serwera
              .AllowAnyMethod()                     // Zezwalamy na dowoln¹ metodê HTTP
              .AllowAnyHeader()                     // Zezwalamy na dowolne nag³ówki
    );
});

// Rejestracja kontrolerów
builder.Services.AddControllers();

var app = builder.Build();

// U¿ycie CORS z polityk¹ "AllowFrontend"
app.UseCors("AllowFrontend");

// Ustawienie adresu URL dla backendu (zezwolenie na dostêp z zewn¹trz)
app.Urls.Add("http://0.0.0.0:5000");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskContext>();
    db.Database.Migrate();
}

// Mapowanie kontrolerów
app.MapControllers();

// Uruchomienie aplikacji
app.Run();
