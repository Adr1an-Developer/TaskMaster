﻿using Autofac;
using TaskMaster.Domain.Data.Abstractions;
using TaskMaster.Domain.Data.Repository;

namespace TaskMaster.AutofacModule
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ValidateUserRepository>().As<IValidateUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerLifetimeScope();
        }
    }
}