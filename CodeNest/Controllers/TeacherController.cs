using CodeNest.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Controllers;

[RoleAuthorize("Teacher")]
public class TeacherController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("UserRole") != "Teacher") return RedirectToAction("Login", "Account");
        return View();
    }

}