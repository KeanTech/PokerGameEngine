using Microsoft.AspNetCore.Mvc;

namespace GameEngine.Controllers
{
    public class DummyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
