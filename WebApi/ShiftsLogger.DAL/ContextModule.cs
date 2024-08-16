using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShiftsLogger.DAL;
public class ContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c =>
        {
            var configuration = c.Resolve<IConfiguration>();
            var optionsBuilder = new DbContextOptionsBuilder<ShiftsContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
            return new ShiftsContext(optionsBuilder.Options);
        })
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
