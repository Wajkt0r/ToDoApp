using backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodanie dost�pu do bazy PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseNpgsql(connectionString));

// Konfiguracja CORS (zezwalamy na dost�p z frontendu)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:8080")  // Zezwalamy na dost�p z frontendowego serwera
              .AllowAnyMethod()                     // Zezwalamy na dowoln� metod� HTTP
              .AllowAnyHeader()                     // Zezwalamy na dowolne nag��wki
    );
});

// Rejestracja kontroler�w
builder.Services.AddControllers();

var app = builder.Build();

// U�ycie CORS z polityk� "AllowFrontend"
app.UseCors("AllowFrontend");

// Ustawienie adresu URL dla backendu (zezwolenie na dost�p z zewn�trz)
app.Urls.Add("http://0.0.0.0:5000");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskContext>();
    db.Database.Migrate();
}

// Mapowanie kontroler�w
app.MapControllers();

// Uruchomienie aplikacji
app.Run();
