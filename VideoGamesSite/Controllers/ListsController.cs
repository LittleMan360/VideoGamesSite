using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VideoGamesSite.Data;
using VideoGamesSite.Models;

namespace VideoGamesSite.Controllers
{
    public class ListsController : Controller
    {
        private readonly AppDbContext _db;

        public ListsController(AppDbContext db)
        {
            _db = db;
        }

        // TOP 10 
        public IActionResult Top10()
        {
            var model = _db.TopGames
                .OrderBy(x => x.Rank)
                .ThenBy(x => x.Id)
                .ToList();

            return View(model);
        }

        // POST: /Lists/AddTopGame
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTopGame(string title, string? tag)
        {
            // Enforce max 10
            var count = _db.TopGames.Count();
            if (count >= 10 || string.IsNullOrWhiteSpace(title))
            {
                return RedirectToAction(nameof(Top10));
            }

            var maxRank = count > 0 ? _db.TopGames.Max(x => x.Rank) : 0;

            var game = new TopGame
            {
                Title = title.Trim(),
                Tag = string.IsNullOrWhiteSpace(tag) ? null : tag.Trim(),
                Rank = maxRank + 1
            };

            _db.TopGames.Add(game);
            _db.SaveChanges();

            return RedirectToAction(nameof(Top10));
        }

        // POST: /Lists/RemoveTopGame
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveTopGame(int id)
        {
            var game = _db.TopGames.FirstOrDefault(x => x.Id == id);
            if (game != null)
            {
                _db.TopGames.Remove(game);
                _db.SaveChanges();

                // Normalize ranks after removal
                var remaining = _db.TopGames
                    .OrderBy(x => x.Rank)
                    .ThenBy(x => x.Id)
                    .ToList();

                for (int i = 0; i < remaining.Count; i++)
                {
                    remaining[i].Rank = i + 1;
                }

                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Top10));
        }

        // POST: /Lists/MoveTopGame
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveTopGame(int id, string direction)
        {
            var items = _db.TopGames
                .OrderBy(x => x.Rank)
                .ThenBy(x => x.Id)
                .ToList();

            var index = items.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return RedirectToAction(nameof(Top10));
            }

            var newIndex = direction == "up" ? index - 1 : index + 1;
            if (newIndex < 0 || newIndex >= items.Count)
            {
                return RedirectToAction(nameof(Top10));
            }

            var current = items[index];
            var target = items[newIndex];

            var tempRank = current.Rank;
            current.Rank = target.Rank;
            target.Rank = tempRank;

            _db.SaveChanges();

            return RedirectToAction(nameof(Top10));
        }

        // NOW PLAYING 
        public IActionResult NowPlaying()
        {
            var model = _db.NowPlayingItems
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Id)
                .ToList();

            return View(model);
        }

        // BACKLOG – your existing Razor/JS page
        public IActionResult Backlog() => View();

        // POST: /Lists/AddNowPlaying
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNowPlaying(string title, string platform, string? focus, int progress)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return RedirectToAction(nameof(NowPlaying));
            }

            var maxOrder = _db.NowPlayingItems.Any()
                ? _db.NowPlayingItems.Max(x => x.DisplayOrder)
                : 0;

            var item = new NowPlayingItem
            {
                Title = title.Trim(),
                Platform = string.IsNullOrWhiteSpace(platform) ? "Other" : platform.Trim(),
                Focus = string.IsNullOrWhiteSpace(focus) ? null : focus.Trim(),
                Progress = Math.Clamp(progress, 0, 100),
                DisplayOrder = maxOrder + 1
            };

            _db.NowPlayingItems.Add(item);
            _db.SaveChanges();

            return RedirectToAction(nameof(NowPlaying));
        }

        // POST: /Lists/UpdateProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProgress(int id, int progress)
        {
            var item = _db.NowPlayingItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Progress = Math.Clamp(progress, 0, 100);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(NowPlaying));
        }

        // POST: /Lists/RemoveNowPlaying
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveNowPlaying(int id)
        {
            var item = _db.NowPlayingItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _db.NowPlayingItems.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(NowPlaying));
        }

        // POST: /Lists/MoveNowPlaying
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveNowPlaying(int id, string direction)
        {
            var items = _db.NowPlayingItems
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Id)
                .ToList();

            var index = items.FindIndex(x => x.Id == id);
            if (index == -1)
            {
                return RedirectToAction(nameof(NowPlaying));
            }

            var newIndex = direction == "up" ? index - 1 : index + 1;
            if (newIndex < 0 || newIndex >= items.Count)
            {
                return RedirectToAction(nameof(NowPlaying));
            }

            var current = items[index];
            var target = items[newIndex];

            var tempOrder = current.DisplayOrder;
            current.DisplayOrder = target.DisplayOrder;
            target.DisplayOrder = tempOrder;

            _db.SaveChanges();

            return RedirectToAction(nameof(NowPlaying));
        }
    }
}

