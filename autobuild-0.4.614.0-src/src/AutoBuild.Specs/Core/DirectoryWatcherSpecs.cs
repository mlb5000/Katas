using System;
using System.IO;
using System.Reflection;
using System.Threading;
using AutoBuild.Core;
using Machine.Specifications;
using Rhino.Mocks;

namespace AutoBuild.Specs.Core
{
    [Subject(typeof(DirectoryWatcher))]
    public class when_when_watching_a_path_for_file_changes : DirectoryWatcherSpecs
    {
        Because of = () =>
            Subject.Watch();

        It should_set_file_system_watcher_path = () =>
            fileSystemWatcher.Path.ShouldEqual(absolutePath);

        It should_enable_file_system_watcher = () =>
            fileSystemWatcher.EnableRaisingEvents.ShouldBeTrue();

        It should_send_info_message_to_console = () =>
            console.WasCalled(c => c.Info(Arg<string>.Is.Anything));
    }

    [Subject(typeof(DirectoryWatcher))]
    public class when_watching_a_relative_path : DirectoryWatcherSpecs
    {
        Establish context = () =>
            executionArguments = new ExecutionArguments("filePath", relativePath);

        Because of = () =>
            Subject.Watch();

        It should_set_absolute_file_system_watcher_path = () =>
            fileSystemWatcher.Path.ShouldEqual(absolutePath);
    }

    [Subject(typeof(DirectoryWatcher))]
    public class when_disabling_the_watcher : DirectoryWatcherSpecs
    {
        Establish context = () =>
        {
            fileSystemWatcher.Path = absolutePath;
            fileSystemWatcher.EnableRaisingEvents = true;
        };

        Because of = () =>
            Subject.Disable();

        It should_disable_the_file_watcher = () =>
            fileSystemWatcher.EnableRaisingEvents.ShouldBeFalse();

        Cleanup after = () =>
            fileSystemWatcher.EnableRaisingEvents = false;
    }

    [Subject(typeof(DirectoryWatcher))]
    public class when_a_file_is_changed : DirectoryWatcherSpecs
    {
        static bool fileChangedEventWasRaised;

        Establish context = () =>
        {
            fileSystemWatcher.Path = absolutePath;
            fileSystemWatcher.EnableRaisingEvents = true;
            fileChangedFilter.Stub(f => f.IsMatch(Arg<string>.Is.Anything)).Return(true);
            Subject.FileChanged += (sender, e) => fileChangedEventWasRaised = true;
        };

        Because of = () =>
        {
            var writer = File.AppendText(filePath);
            writer.WriteLine("hello");
            writer.Close();
            Thread.Sleep(1000);
        };

        It should_raise_file_changed_event = () =>
            fileChangedEventWasRaised.ShouldBeTrue();

        Cleanup after = () =>
        {
            if (File.Exists(filePath)) File.Delete(filePath);
        };
    }

    public abstract class DirectoryWatcherSpecs : Specification<DirectoryWatcher>
    {
        protected static ExecutionArguments executionArguments;
        protected static FileSystemWatcher fileSystemWatcher;
        protected static IFileChangedFilter fileChangedFilter;
        protected static IConsole console;
        protected static string absolutePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        protected static string relativePath = @".";
        protected static string filePath = Path.Combine(absolutePath, "text.txt");

        Establish context = () =>
        {
            executionArguments = new ExecutionArguments("filePath", absolutePath);
            fileSystemWatcher = new FileSystemWatcher();
            fileChangedFilter = Mock<IFileChangedFilter>();
            console = Mock<IConsole>();
            Subject = new DirectoryWatcher(executionArguments, fileSystemWatcher, fileChangedFilter, console);
        };
    }
}