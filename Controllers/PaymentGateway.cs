using Microsoft.AspNetCore.Mvc;

namespace SprintHEMone.Controllers
{
    public class PaymentGateway : Controller
    {
        public IActionResult Index()
        {
            return View("PaymentGateway");
        }
    }
}
