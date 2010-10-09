using AutoBuild.Console;
using AutoBuild.Core;
using Castle.Windsor;
using Machine.Specifications;

namespace AutoBuild.Specs.Console
{
    [Subject(typeof (WindsorConfiguration))]
    public class when_configuring_the_container_without_execution_arguments : WindsorConfigurationSpecs
    {
        Because of = () =>
            Subject.ConfigureContainer();

        Behaves_like<ContainerBehaviour> a_container;
    }

    [Subject(typeof (WindsorConfiguration))]
    public class when_configuring_the_container_with_execution_arguments : WindsorConfigurationSpecs
    {
        Because of = () =>
            Subject.ConfigureContainer(executionArguments);

        It should_register_execution_arguments_with_container = () =>
            container.Resolve<ExecutionArguments>();

        Behaves_like<ContainerBehaviour> a_container;
    }

    [Behaviors]
    public class ContainerBehaviour
    {
        protected static WindsorContainer container;

        It should_resolve_all_components = () =>
        {
            foreach (var handler in container.Kernel.GetAssignableHandlers(typeof(object)))
            {
                container.Resolve(handler.Service);
            }
        };

        It should_resolve_root_autobuild_runner_type = () =>
            container.Resolve<AutoBuildRunner>();
    }

    public abstract class WindsorConfigurationSpecs : Specification<WindsorConfiguration>
    {
        protected static WindsorContainer container;
        protected static ExecutionArguments executionArguments;

        Establish context = () =>
        {
            container = new WindsorContainer();
            executionArguments = new ExecutionArguments("a build file", "a watch path");
            Subject = new WindsorConfiguration(container);
        };
    }
}