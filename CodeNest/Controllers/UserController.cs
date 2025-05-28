using CodeNest.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Controllers;

[RoleAuthorize("User")]
public class UserController : Controller
{
    // GET
    public IActionResult Dashboard() => View();
}