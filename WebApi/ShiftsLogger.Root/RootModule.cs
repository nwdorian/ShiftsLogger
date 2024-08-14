using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ShiftsLogger.DAL;
using ShiftsLogger.Repository;
using ShiftsLogger.Repository.Profiles;

namespace ShiftsLogger.Root;
public class RootModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(typeof(UserProfile).Assembly);
        builder.RegisterModule<RepositoryModule>();
        builder.RegisterModule<ContextModule>();
    }
}
