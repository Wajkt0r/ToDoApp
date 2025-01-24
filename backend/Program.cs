using backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:8080") 
              .AllowAnyMethod()                     
              .AllowAnyHeader()                     
    );
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.Urls.Add("http://0.0.0.0:5000");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskContext>();
    db.Database.Migrate();
}

app.MapControllers();

app.Run();
