using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
