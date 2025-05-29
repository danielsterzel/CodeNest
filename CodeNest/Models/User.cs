using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class User
{
    [Required] [Key] public int UserId { get; set; }
    [Required] [StringLength(50)] public required string UserName { get; set; }  
    [Required] [StringLength(50)] public required string FirstName { get; set; }
    [Required] [StringLength(50)] public required string LastName { get; set; }
    [Required] [StringLength(35)] public required string Password { get; set; }
    [Required] [StringLength(15)] public required string Phone { get; set; }
    [Required] [StringLength(50)] public required string Email { get; set; }
    public  List<Course> Courses { get; set; } = new List<Course>();
    public  List<Reservation> Reservations { get; set; } = new List<Reservation>();
    [Required] [StringLength(10)] public required string Role { get; set; }

}