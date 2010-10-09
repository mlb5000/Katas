using System.IO;
using System.Reflection;
using AutoBuild.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AutoBuild.Console
{
    public class WindsorConfiguration
    {
        private readonly WindsorContainer container;
        private readonly Assembly coreAssembly;

        public WindsorConfiguration(WindsorContainer container)
        {
            this.container = container;
            coreAssembly = typeof (AutoBuildRunner).Assembly;
        }

        public void ConfigureContainer()
        {
            ConfigureContainer(new EmptyExecutionArguments());
        }

        public void ConfigureContainer(ExecutionArguments executionArguments)
        {
            container.Kernel.Resolver.AddSubResolver(new ArraySubDependencyResolver(container.Kernel));

            container
                .Register(Component
                              .For<AutoBuildRunner>()
                              .LifeStyle.Transient)
                .Register(Component
                              .For<ExecutionArguments>()
                              .Instance(executionArguments))
                .Register(Component
                              .For<FileSystemWatcher>()
                              .ImplementedBy<FileSystemWatcher>()
                              .LifeStyle.Transient)
                .Register(AllTypes.FromAssembly(coreAssembly)
                              .Where(t => t.GetInterfaces().Length > 0)
                              .WithService.FirstInterface()
                              .Configure(config => config.LifeStyle.Transient));
        }
    }
}