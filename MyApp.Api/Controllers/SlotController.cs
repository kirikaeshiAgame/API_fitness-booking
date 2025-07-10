using Microsoft.AspNetCore.Mvc;
using MyApp.Api.Models;

namespace MyApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SlotController : ControllerBase
{
    private static readonly List<TrainingSlot> Slots = new()
    {
        new TrainingSlot { Id = 1, StartTime = DateTime.Today.AddHours(10), MaxClients = 5 },
        new TrainingSlot { Id = 2, StartTime = DateTime.Today.AddHours(18), MaxClients = 10 },
        new TrainingSlot { Id = 3, StartTime = DateTime.Today.AddDays(1).AddHours(10), MaxClients = 5 },
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Slots.Select(s => new
        {
            s.Id,
            s.StartTime,
            s.MaxClients,
            Booked = s.Bookings.Count
        }));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var slot = Slots.FirstOrDefault(s => s.Id == id);
        if (slot == null) return NotFound();
        return Ok(slot);
    }

    public static TrainingSlot? GetSlot(int id) => Slots.FirstOrDefault(s => s.Id == id);
}
