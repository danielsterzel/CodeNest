using CodeNest.Attributes;
using CodeNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeNest.Controllers;

[RoleAuthorize("User")]
public class UserController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;
    
    public List<DateTime> GenerateWeeklyLessons(DateTime start, DateTime end)
    {
        var lessonSchedule = new List<DateTime>();
        var current = start;
        while (current <= end)
        {
            lessonSchedule.Add(current);
            current = current.AddDays(7);
        }

        return lessonSchedule;
    }

    public User GetUser()
    {
        var id = HttpContext.Session.GetString("UserId");
        
        int usrId = int.Parse(id!);

        var usr = _context.Users.Include(u => u.Courses)
            .FirstOrDefault(u => u.UserId == usrId)!;
        return usr;
    }

    // GET
    [HttpGet]
    public IActionResult Dashboard() => View();

    // [HttpGet]
    // public IActionResult ListCourses()
    // {
    // }
    
    [HttpGet]
    public IActionResult Calendar()
    {
        var usr = GetUser();
        var courseList = usr.Courses.Select(c => new CourseScheduleViewModel
        {
            Course = c,
            LessonDates = GenerateWeeklyLessons(c.StartDate,c.EndDate)
        }).ToList();

        return View(courseList);
    }

}