using System;
using AutoBuild.Core;
using Machine.Specifications;
using Rhino.Mocks;

namespace AutoBuild.Specs.Core
{
    [Subject(typeof(NAntBuildRunner))]
    public class when_creating_a_nant_build_runner : NAntBuildRunnerSpecs
    {
        Establish context = () =>
        {
            pathResolver = Mock<IPathResolver>();
            Subject = new NAntBuildRunner(executionArguments, configSettings, pathResolver, console) { CreateNoWindow = true };
        };

        It should_resolve_nant_exe_path = () =>
            pathResolver.WasCalled(p => p.Resolve(configSettings.NAntExePath));
    }

    [Subject(typeof(NAntBuildRunner))]
    public class when_running_build : NAntBuildRunnerSpecs
    {
        protected static bool buildStartedEventWasRaised;
        protected static bool buildCompletedEventWasRaised;

        Establish context = () =>
        {
            Subject.BuildStarted += (sender, e) => buildStartedEventWasRaised = true;
            Subject.BuildCompleted += (sender, e) => buildCompletedEventWasRaised = true;
        };

        Because of = () =>
            Subject.RunBuild();

        It should_raise_build_started_event = () =>
            buildStartedEventWasRaised.ShouldBeTrue();

        It should_raise_build_completed_event = () =>
            buildCompletedEventWasRaised.ShouldBeTrue();

        It should_not_send_error_message_to_console = () =>
            console.WasNotCalled(l => l.Error(null, null), c => c.IgnoreArguments());
    }

    [Subject(typeof(NAntBuildRunner))]
    public class when_an_error_occurs_starting_the_build_process : NAntBuildRunnerSpecs
    {
        Establish context = () =>
        {
            configSettings.NAntExePath = "BadFilePath";
            Subject = new NAntBuildRunner(executionArguments, configSettings, pathResolver, console) {CreateNoWindow = true};
        };

        Because of = () =>
            Subject.RunBuild();

        It should_send_error_message_to_console = () =>
            console.WasCalled(l => l.Error(Arg<string>.Is.Anything, Arg<Exception>.Is.NotNull));
    }

    public abstract class NAntBuildRunnerSpecs : Specification<NAntBuildRunner>
    {
        protected static ExecutionArguments executionArguments;
        protected static IConfigurationSettings configSettings;
        protected static IPathResolver pathResolver;
        protected static IConsole console;

        Establish context = () =>
        {
            executionArguments = new ExecutionArguments("test.build", "somewatchdirectory");
            configSettings = new ConfigurationSettings();
            pathResolver = new PathResolver();
            console = Mock<IConsole>();
            Subject = new NAntBuildRunner(executionArguments, configSettings, pathResolver, console) { CreateNoWindow = true };
        };
    }
}