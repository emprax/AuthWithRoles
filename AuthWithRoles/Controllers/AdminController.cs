using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthWithRoles.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            ViewData["PageID"] = "Admin";
            ViewData["Message"] = "Admin page restricted to the super admin user";
            return View();
        }

        public IActionResult Settings()
        {
            ViewData["Message"] = "Settings page restricted to the super admin user";
            return View();
        }
    }
}