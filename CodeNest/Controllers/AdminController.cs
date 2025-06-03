using CodeNest.Attributes;
using CodeNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeNest.Controllers;

[RoleAuthorize("Admin")]
public class AdminController(AppDbContext context) : Controller
{


    private readonly AppDbContext _context = context;

    // GET
    public IActionResult AdminDashboard()
    {
        return View();

    }

    [HttpGet]
    public IActionResult CreateCourse()
    {
        var teachers = _context.Teachers.Select(t => new SelectListItem
            {
                Value = t.TeacherId.ToString(),
                Text = t.UserName // or any other name field you want to show
            })
            .ToList();
        ViewBag.Subjects = new List<SelectListItem>
        {
            new SelectListItem { Value = "Math", Text = "Matematyka" },
            new SelectListItem { Value = "Programming", Text = "Programowanie" },
            new SelectListItem { Value = "Physics", Text = "Fizyka" },
            new SelectListItem { Value = "English", Text = "Angielski" }
        };

        ViewBag.Teachers = teachers;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCourse(Course model)
    {
        if(ModelState.IsValid)
        {
            _context.Courses.Add(model);
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == model.TeacherId);
            if (teacher != null)
            {
                teacher.Courses.Add(model);
            }

            _context.SaveChanges();
            
            TempData["SuccessMessage"] = "Kurs stworzony pomyślnie!";
            return RedirectToAction("CreateCourse");
        }
        ViewBag.Teachers = _context.Teachers
            .Select(t => new SelectListItem
            {
                Value = t.TeacherId.ToString(),
                Text = t.UserName
            })
            .ToList();
        ViewBag.Subjects = new List<SelectListItem>
        {
            new SelectListItem { Value = "Math", Text = "Matematyka" },
            new SelectListItem { Value = "Programming", Text = "Programowanie" },
            new SelectListItem { Value = "Physics", Text = "Fizyka" },
            new SelectListItem { Value = "English", Text = "Angielski" }
        };


        return View(model);
    }
}