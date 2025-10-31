using Microsoft.AspNetCore.Mvc;

namespace VideoGamesSite.Controllers
{
    public class ListsController : Controller
    {
        // GET /Lists/Top10  (already works)
        public IActionResult Top10() => View();

        // GET /Lists/NowPlaying
        public IActionResult NowPlaying() => View();

        // GET /Lists/Backlog
        public IActionResult Backlog() => View();
    }
}