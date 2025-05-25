using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Course
{
    [Required] public required int CourseId { get; set; }
    public required List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public required List<Student> Students { get; set; } = new List<Student>();
    [Required] [StringLength(50)] public required string Name { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] [StringLength(50)] public required string Subject { get; set; }
    [Required] public required int TeacherId { get; set; }
    [Required] public required Teacher Teacher { get; set; }

}