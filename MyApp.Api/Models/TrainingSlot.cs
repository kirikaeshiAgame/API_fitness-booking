namespace MyApp.Api.Models
{
    public class TrainingSlot
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int MaxClients { get; set; }
        public List<Booking> Bookings { get; set; } = new();
    }
}
