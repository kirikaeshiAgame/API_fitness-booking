using System.Net.Http;
using System.Text;
using System.Text.Json;
using MyApp.Api.Models;

namespace MyApp.Api.Services;

public class TelegramNotifier
{
    private readonly string _botToken = ""; // вставь свой токен
    private readonly string _chatId = "";     // вставь свой chat_id

    public async Task SendNotificationAsync(Booking booking, DateTime slotTime)
    {
        using var httpClient = new HttpClient();

        var message = $"📅 Новая запись:\n" +
                      $"Имя: {booking.Name}\n" +
                      $"Телефон: {booking.Phone}\n" +
                      $"Время: {slotTime:dd.MM.yyyy HH:mm}";

        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            chat_id = _chatId,
            text = message
        }), Encoding.UTF8, "application/json");

        await httpClient.PostAsync(url, content);
    }
}
