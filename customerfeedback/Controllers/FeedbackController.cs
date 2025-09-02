using Microsoft.AspNetCore.Mvc;
using CustomerFeedback.Models;
namespace CustomerFeedback.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Thank you for your feedback! Your input is valuable to us.";
                return RedirectToAction("Index");
            }
            TempData["SuccessMessage"] = "Thank you for your valuable feedback!";
            return View("Index", feedback);
        }
    }
}
