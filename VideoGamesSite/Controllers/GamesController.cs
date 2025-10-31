using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGamesSite.Data;
using VideoGamesSite.Models;
using VideoGamesSite.ViewModels;

namespace VideoGamesSite.Controllers
{
    public class GamesController(AppDbContext db) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var games = await db.Games.Include(g => g.Platform).OrderBy(g => g.Title).ToListAsync();
            return View(games);
        }

        public async Task<IActionResult> Details(int id)
        {
            var game = await db.Games.Include(g => g.Platform).FirstOrDefaultAsync(g => g.Id == id);
            return game is null ? NotFound() : View(game);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new GameFormViewModel { Platforms = await PlatformSelectList() };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameFormViewModel vm)
        {
            if (!ModelState.IsValid) { vm.Platforms = await PlatformSelectList(); return View(vm); }

            db.Games.Add(new Game
            {
                Title = vm.Title,
                Genre = vm.Genre,
                ReleaseYear = vm.ReleaseYear,
                HoursPlayed = vm.HoursPlayed,
                PlatformId = vm.PlatformId
            });
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var g = await db.Games.FindAsync(id);
            if (g is null) return NotFound();
            var vm = new GameFormViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Genre = g.Genre,
                ReleaseYear = g.ReleaseYear,
                HoursPlayed = g.HoursPlayed,
                PlatformId = g.PlatformId,
                Platforms = await PlatformSelectList()
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GameFormViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) { vm.Platforms = await PlatformSelectList(); return View(vm); }

            var g = await db.Games.FindAsync(id);
            if (g is null) return NotFound();

            g.Title = vm.Title; g.Genre = vm.Genre; g.ReleaseYear = vm.ReleaseYear;
            g.HoursPlayed = vm.HoursPlayed; g.PlatformId = vm.PlatformId;

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var g = await db.Games.Include(x => x.Platform).FirstOrDefaultAsync(x => x.Id == id);
            return g is null ? NotFound() : View(g);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var g = await db.Games.FindAsync(id);
            if (g is null) return NotFound();
            db.Games.Remove(g);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> PlatformSelectList() =>
            await db.Platforms.OrderBy(p => p.Name)
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                .ToListAsync();
    }
}
