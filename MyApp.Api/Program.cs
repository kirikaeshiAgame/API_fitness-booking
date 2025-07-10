using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using MyApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ?? ���������� CORS ��� ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ?? ���������� �����������
builder.Services.AddControllers();

// ?? ���������� �������� EF � PostgreSQL
builder.Services.AddDbContext<FitnessContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? ���������� TelegramNotifier ��� ������
builder.Services.AddScoped<TelegramNotifier>();

var app = builder.Build();

// ?? ���������� CORS
app.UseCors("AllowAll");

// ?? ����������� (����� ��������, ���� ���� �� �����������)
app.UseAuthorization();

// ?? ����� �����������
app.MapControllers();

// ?? ���������
app.Run();
