using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ShiftsLogger.DAL;
public class ShiftsContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ShiftEntity> Shifts { get; set; }

    public ShiftsContext(DbContextOptions<ShiftsContext> options) : base(options)
    {

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
    }
}
