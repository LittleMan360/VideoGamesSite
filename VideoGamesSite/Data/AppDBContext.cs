using Microsoft.EntityFrameworkCore;
using VideoGamesSite.Models;

namespace VideoGamesSite.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Platform> Platforms => Set<Platform>();

        public DbSet<NowPlayingItem> NowPlayingItems => Set<NowPlayingItem>();
        public DbSet<TopGame> TopGames => Set<TopGame>();

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

            b.Entity<TopGame>().HasData(
                new TopGame { Id = 1, Title = "Halo: Reach", Tag = "FPS", Rank = 1 },
                new TopGame { Id = 2, Title = "Halo 3", Tag = "FPS", Rank = 2 },
                new TopGame { Id = 3, Title = "Gears of War 2", Tag = "TPS", Rank = 3 },
                new TopGame { Id = 4, Title = "Borderlands 2", Tag = "Looter Shooter", Rank = 4 },
                new TopGame { Id = 5, Title = "Call of Duty: Black Ops II", Tag = "FPS", Rank = 5 },
                new TopGame { Id = 6, Title = "The Witcher 3: Wild Hunt", Tag = "RPG", Rank = 6 },
                new TopGame { Id = 7, Title = "Baldur's Gate 3", Tag = "CRPG", Rank = 7 },
                new TopGame { Id = 8, Title = "Elden Ring", Tag = "Action RPG", Rank = 8 },
                new TopGame { Id = 9, Title = "RimWorld", Tag = "Colony Sim", Rank = 9 },
                new TopGame { Id = 10, Title = "Psychonauts", Tag = "Platformer", Rank = 10 }
            );
        }
    }
}
