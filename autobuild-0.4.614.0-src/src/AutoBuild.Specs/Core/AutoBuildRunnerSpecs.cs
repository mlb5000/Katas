using System;
using System.IO;
using AutoBuild.Core;
using Machine.Specifications;
using Rhino.Mocks;

namespace AutoBuild.Specs.Core
{
    [Subject(typeof(AutoBuildRunner))]
    public class when_executing : AutoBuildRunnerSpecs
    {
        Because of = () =>
            Subject.Execute();

        It should_start_watching_directory = () =>
            directoryWatcher.WasCalled(w => w.Watch());
    }

    [Subject(typeof(AutoBuildRunner))]
    public class when_a_file_has_changed : AutoBuildRunnerSpecs
    {
        private const string fileName = "changedfile.cs";

        Because of = () =>
            directoryWatcher.Raise(w => w.FileChanged += null, null, 
                new FileSystemEventArgs(WatcherChangeTypes.Changed, string.Empty, fileName));

        It should_disable_file_watcher = () =>
            directoryWatcher.WasCalled(w => w.Disable());

        It should_send_file_changed_debug_message_to_console = () =>
            console.WasCalled(l => l.Debug(Arg<string>.Is.NotNull));

        It should_run_the_build = () =>
            buildRunner.WasCalled(b => b.RunBuild());
    }

    [Subject(typeof(AutoBuildRunner))]
    public class when_the_build_completes : AutoBuildRunnerSpecs
    {
        Because of = () =>
            buildRunner.Raise(b => b.BuildCompleted += null, null, EventArgs.Empty);

        It should_enable_file_watcher = () =>
            directoryWatcher.WasCalled(w => w.Enable());
    }

    public abstract class AutoBuildRunnerSpecs : Specification<AutoBuildRunner>
    {
        protected static IDirectoryWatcher directoryWatcher;
        protected static IBuildRunner buildRunner;
        protected static IConsole console;

        Establish context = () =>
        {
            directoryWatcher = Mock<IDirectoryWatcher>();
            buildRunner = Mock<IBuildRunner>();
            console = Mock<IConsole>();
            Subject = new AutoBuildRunner(directoryWatcher, buildRunner, console);
        };
    }
}