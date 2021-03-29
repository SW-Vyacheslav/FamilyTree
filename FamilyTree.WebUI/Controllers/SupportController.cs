using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.WebUI.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reference()
        {
            return View();
        }
    }
}