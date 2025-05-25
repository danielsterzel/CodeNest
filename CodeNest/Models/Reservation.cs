using System.ComponentModel.DataAnnotations;

namespace CodeNest.Models;

public class Reservation
{
    [Required] public required int ParentId { get; set; }
    [Required] public required Parent Parent {get; set; }
    [Required] public required int ReservationId { get; set; }
    [Required] public required int CourseId { get; set; }
    [Required] public required Course Course { get; set; }
}