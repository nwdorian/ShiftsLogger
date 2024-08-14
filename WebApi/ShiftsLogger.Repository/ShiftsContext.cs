using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShiftsLogger.Model.Entities;

namespace ShiftsLogger.Repository;
public class ShiftsContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options)
    {

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
    }
}
