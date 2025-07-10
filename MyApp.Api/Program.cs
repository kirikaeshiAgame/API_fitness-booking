using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using MyApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ?? Подключаем CORS для фронта
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ?? Подключаем контроллеры
builder.Services.AddControllers();

// ?? Подключаем контекст EF с PostgreSQL
builder.Services.AddDbContext<FitnessContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? Подключаем TelegramNotifier как сервис
builder.Services.AddScoped<TelegramNotifier>();

var app = builder.Build();

// ?? Используем CORS
app.UseCors("AllowAll");

// ?? Авторизация (можно опустить, если пока не используешь)
app.UseAuthorization();

// ?? Мапим контроллеры
app.MapControllers();

// ?? Запускаем
app.Run();
