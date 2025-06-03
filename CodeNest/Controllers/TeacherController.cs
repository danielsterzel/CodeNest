using CodeNest.Attributes;
using CodeNest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeNest.Controllers;

[RoleAuthorize("Teacher")]
public class TeacherController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;
    // GET
    [HttpGet]
    public IActionResult Dashboard() => View();

    public Teacher GetTeacher()
    {
        var id = HttpContext.Session.GetString("UserId");
        int teacherId = int.Parse(id!);
        var teacher = _context.Teachers
            // we use include because EF normally won't INCLUDE the courses list. This is eager loading meaning we explicitly load
            // related data.
            .Include(t => t.Courses)
            .ThenInclude(c => c.Students)
            .Include(t => t.Courses)
            .ThenInclude(c => c.Reservations)
            .ThenInclude(r => r.User)
            .FirstOrDefault(t => t.TeacherId == teacherId)!;
        return teacher;
    }
    
    public List<DateTime> GenerateWeeklyLessons(DateTime start, DateTime end)
    {
        var lessonSchedule = new List<DateTime>();
        DateTime current = start;
        while (current <= end)
        {
            lessonSchedule.Add(current);
            current = current.AddDays(7);
        }

        return lessonSchedule;
    }
    
    [HttpGet]
    public IActionResult ListAssignedCourses()
    {
        var teacher = GetTeacher();
        var courses = teacher.Courses;
        return View(courses);
    }

 


    [HttpGet]
    public IActionResult Calendar()
    {
        var teacher = GetTeacher();
        var courseList = teacher.Courses.Select(c => new CourseScheduleViewModel
        {
            Course = c,
            LessonDates = GenerateWeeklyLessons(c.StartDate,c.EndDate)
        }).ToList();

        return View(courseList);
    }
}