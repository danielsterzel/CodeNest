using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Student
{
    [Required] public required int StudentId { get; set; }
    [Required] [StringLength(50)] public required string FirstName { get; set; }
    [Required] [StringLength(50)] public required string LastName { get; set; }
    [Required] public required int ParentId { get; set; }
    [Required] public required Parent Parent { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();

}