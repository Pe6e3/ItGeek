using Microsoft.AspNetCore.Mvc;

namespace ItGeek.Web.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
