using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Teacher
{
    [Required] public required int TeacherId { get; set; }
    [Required][StringLength(50)] public required string Name { get; set; }
    [Required] [StringLength(50)] public required string LastName { get; set; }
    [Required] [StringLength(50)] public required string Email { get; set; }
    [Required] [StringLength(15)] public required string Phone { get; set; }
    [Required] [StringLength(35)] public required string Password { get;set;}
    public List<Course> Courses { get; set; } = new List<Course>();
}