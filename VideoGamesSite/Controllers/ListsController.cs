using Microsoft.AspNetCore.Mvc;

namespace VideoGamesSite.Controllers
{
    public class ListsController : Controller
    {
        // /Lists/Top10
        public IActionResult Top10() => View();
    }
}