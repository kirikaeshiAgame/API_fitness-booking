using Microsoft.EntityFrameworkCore;
using MyApp.Api.Models;

namespace WebApi.Data
{
    public class FitnessContext : DbContext
    {
        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }

        public DbSet<TrainingSlot> TrainingSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingSlot>()
                .HasMany(s => s.Bookings)
                .WithOne()
                .HasForeignKey(b => b.TrainingSlotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
