using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Parent
{
    [Required] [Key] public int ParentId { get; set; }
    [Required] [StringLength(50)] public required string FirstName { get; set; }
    [Required] [StringLength(50)] public required string LastName { get; set; }
    [Required] [StringLength(35)] public required string Password { get; set; }
    [Required] [StringLength(15)] public required string Phone { get; set; }
    [Required] [StringLength(50)] public required string Email { get; set; }
    public  List<Student> Children { get; set; } = new List<Student>();
    public  List<Reservation> Reservations { get; set; } = new List<Reservation>();

}