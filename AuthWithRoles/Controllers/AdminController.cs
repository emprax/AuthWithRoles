using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthWithRoles.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewData["PageID"] = "Admin";
            ViewData["Message"] = "admin page restricted to the super admin user ";
            return View();
        }

        public IActionResult Settings()
        {
            ViewData["Message"] = "just another page restricted to the super admin user ";
            return View();
        }
    }
}