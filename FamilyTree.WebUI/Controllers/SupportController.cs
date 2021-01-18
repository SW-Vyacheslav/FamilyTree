using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.WebUI.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Reference()
        {
            return View();
        }
    }
}