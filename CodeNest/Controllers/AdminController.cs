using Microsoft.AspNetCore.Mvc;

namespace CodeNest.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult AdminDashboard() => View();
}