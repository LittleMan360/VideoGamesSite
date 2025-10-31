using Microsoft.AspNetCore.Mvc;

namespace VideoGamesSite.Controllers
{
    public class AboutController : Controller
    {
        // GET /About/Me
        public IActionResult Me() => View();

        // GET /About/Hobby
        public IActionResult Hobby() => View();
    }
}