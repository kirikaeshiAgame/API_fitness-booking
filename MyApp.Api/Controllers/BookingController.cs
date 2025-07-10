using Microsoft.AspNetCore.Mvc;
using MyApp.Api.Models;
using MyApp.Api.Services;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly FitnessContext _context;
    private readonly TelegramNotifier _notifier;

    public BookingController(FitnessContext context, TelegramNotifier notifier)
    {
        _context = context;
        _notifier = notifier;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Booking booking)
    {
        // Получаем слот из БД с его записями
        var slot = await _context.TrainingSlots
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == booking.TrainingSlotId);

        if (slot == null)
            return NotFound("Слот не найден");

        if (slot.Bookings.Count >= slot.MaxClients)
            return BadRequest("Запись невозможна — слот заполнен");

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        await _notifier.SendNotificationAsync(booking, slot.StartTime);

        return Ok(new { status = "Запись успешно создана" });
    }
}
