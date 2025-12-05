using Microsoft.EntityFrameworkCore;
using VideoGamesSite.Models;

namespace VideoGamesSite.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Platform> Platforms => Set<Platform>();

        // 🔹 Add this:
        public DbSet<NowPlayingItem> NowPlayingItems => Set<NowPlayingItem>();

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
            
            // Games
            b.Entity<Game>().HasData(
                new Game { Id = 1, Title = "Elden Ring", Genre = "Action RPG", ReleaseYear = 2022, HoursPlayed = 120, PlatformId = 1 },
                new Game { Id = 2, Title = "Hades", Genre = "Roguelike", ReleaseYear = 2020, HoursPlayed = 40, PlatformId = 4 }
            );

            //Now playing games
            b.Entity<NowPlayingItem>().HasData(
                new NowPlayingItem
                {
                    Id = 1,
                    Title = "Halo CE",
                    Platform = "Xbox",
                    Focus = "Beating story with girlfiend",
                    Progress = 20,
                    DisplayOrder = 1
                },
                new NowPlayingItem
                {
                    Id = 2,
                    Title = "Ark Survival Ascended",
                    Platform = "PC",
                    Focus = "Beating the bosses",
                    Progress = 0,
                    DisplayOrder = 2
                }
            );
        }
    }
}
