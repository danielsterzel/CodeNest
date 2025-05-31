using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Reservation
{
    [Required] public required int UserId { get; set; }
    [Required] public required User User {get; set; }
    [Required] [Key] public  int ReservationId { get; set; }
    [Required] public required int CourseId { get; set; }
    [Required] public required Course Course { get; set; }
}