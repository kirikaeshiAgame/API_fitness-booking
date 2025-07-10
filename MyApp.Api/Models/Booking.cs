namespace MyApp.Api.Models;

public class Booking
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int TrainingSlotId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
