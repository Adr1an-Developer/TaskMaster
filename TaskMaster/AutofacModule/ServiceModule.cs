using Autofac;
using TaskMaster.Domain.Services.Abstractions;
using TaskMaster.Domain.Services.Implementations;

namespace TaskMaster.AutofacModule
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ServiceBase<>))
            .As(typeof(IServiceBase<>));

            builder
                  .RegisterType<ValidateUserService>()
                  .As<IValidateUserService>()
                  .InstancePerLifetimeScope();
            builder
                 .RegisterType<ProjectService>()
                 .As<IProjectService>()
                 .InstancePerLifetimeScope();
            builder
                 .RegisterType<TaskService>()
                 .As<ITaskService>()
                 .InstancePerLifetimeScope();
        }
    }
}