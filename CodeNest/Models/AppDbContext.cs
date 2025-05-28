using Microsoft.EntityFrameworkCore;

namespace CodeNest.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Course> Courses { get; set; }

   
}
