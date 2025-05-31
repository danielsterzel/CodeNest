namespace CodeNest.Models;

public class CourseScheduleViewModel
{
    public required Course Course { get; set; }
    public required List<DateTime> LessonDates { get; set; }
}