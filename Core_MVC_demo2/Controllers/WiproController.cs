using Microsoft.AspNetCore.Mvc;

namespace Core_MVC_demo2.Controllers
{
    public class WiproController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult tag_helpers()
        {
            return View();
        }   

        public IActionResult form_validation()
        {
            return View();
        }
    }
}
