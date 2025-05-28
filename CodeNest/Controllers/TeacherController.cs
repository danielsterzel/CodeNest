using CodeNest.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Controllers;

[RoleAuthorize("Teacher")]
public class TeacherController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Dashboard() => View();
    
}