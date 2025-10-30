using Microsoft.EntityFrameworkCore;
using VideoGamesSite.Models;

namespace VideoGamesSite.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Platform> Platforms => Set<Platform>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // Seed Platforms
            b.Entity<Platform>().HasData(
                new Platform { Id = 1, Name = "PC" },
                new Platform { Id = 2, Name = "PS5" },
                new Platform { Id = 3, Name = "Xbox" },
                new Platform { Id = 4, Name = "Switch" }
            );

            // Optional seed Games
            b.Entity<Game>().HasData(
                new Game { Id = 1, Title = "Elden Ring", Genre = "Action RPG", ReleaseYear = 2022, HoursPlayed = 120, PlatformId = 1 },
                new Game { Id = 2, Title = "Hades", Genre = "Roguelike", ReleaseYear = 2020, HoursPlayed = 40, PlatformId = 4 }
            );
        }
    }
}
