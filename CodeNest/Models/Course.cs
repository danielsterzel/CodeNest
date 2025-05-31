using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Course
{
    [Required] public required int CourseId { get; set; }
    public  List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public  List<User> Students { get; set; } = new List<User>();
    [Required] [StringLength(50)] public required string Name { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] [StringLength(50)] public required string Subject { get; set; }
    public  int TeacherId { get; set; }
    public  Teacher? Teacher { get; set; }

}