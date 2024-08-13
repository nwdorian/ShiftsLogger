using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Model.Entities;

namespace ShiftsLogger.Repository;
public class ShiftsContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Shift> Shift { get; set; }

    public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options)
    {

    }
}
