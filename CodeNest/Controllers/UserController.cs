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

    [HttpGet]
    
    // how does passing into this method work with the cshtml file?
    public IActionResult ListCourses(string? subject)
    {
        ViewBag.SelectedSubject = subject;

        var courses = string.IsNullOrEmpty(subject)
            ? new List<Course>()
            : _context.Courses
                // teacher if we want teacher info
                .Include(c => c.Teacher)
                .Include(c => c.Reservations)
                .Where(c => c.Subject == subject)
                .ToList();
        return View(courses);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SignUp(int courseId)
    {
        var user = GetUser();
        var course = _context.Courses
            .Include(c => c.Reservations)
            .Include(c => c.Students)
            .FirstOrDefault(c => c.CourseId == courseId);
        if (course == null)
        {
            return NotFound("Nie znaleziono kursu z takim identyfikatorem.");
        }

        if (course.Students.Any(s => s.UserId == user.UserId))
        {
            TempData["Message"] = "Jesteś już zapisany na ten kurs!";
            return RedirectToAction("ListCourses", new { subject = course.Subject });
        }


        var reservation = new Reservation
        {
            CourseId = course.CourseId,
            Course = course,
            UserId = user.UserId,
            User = user
        };
        user.Courses.Add(course);
        user.Reservations.Add(reservation);
        
        course.Students.Add(user);
        course.Reservations.Add(reservation);

        _context.Reservations.Add(reservation);
        _context.SaveChanges();
        TempData["Message"] = "Zapisanie zakończone sukcesem!";
        return RedirectToAction("ListCourses", new { subject = course.Subject });

    }

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