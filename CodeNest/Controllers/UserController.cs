using CodeNest.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Controllers;

[RoleAuthorize("User")]
public class UserController : Controller
{
    // GET
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("UserRole") != "User") return RedirectToAction("Login", "Account");
        return View();
    }
}