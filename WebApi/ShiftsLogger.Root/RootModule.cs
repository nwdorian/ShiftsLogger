using Autofac;
using ShiftsLogger.Repository;

namespace ShiftsLogger.Root;
public class RootModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<RepositoryModule>();
    }
}
