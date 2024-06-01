using Microsoft.EntityFrameworkCore;

namespace GameZone.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<GameDevice> GameDevices { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>()
                .HasKey(e => new { e.GameId ,e.DeviceId});
        }
    }
}
