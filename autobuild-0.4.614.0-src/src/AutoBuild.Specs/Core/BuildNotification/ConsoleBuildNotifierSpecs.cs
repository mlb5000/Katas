using AutoBuild.Core;
using AutoBuild.Core.BuildNotification;
using Machine.Specifications;

namespace AutoBuild.Specs.Core.BuildNotification
{
    [Subject(typeof(ConsoleBuildNotifier))]
    public class when_the_build_is_started : ConsoleBuildNotifierSpecs
    {
        Because of = () =>
            Subject.BuildStarted();

        It should_send_build_started_message_to_console = () =>
            console.WasCalled(l => l.Info("Build started"));

        It should_clear_the_message_log = () =>
            messageLogger.WasCalled(l => l.ClearLog());
    }

    [Subject(typeof(ConsoleBuildNotifier))]
    public class when_a_build_message_is_logged : ConsoleBuildNotifierSpecs
    {
        const string message = "A build message";

        Because of = () =>
            Subject.MessageLogged(message);

        It should_log_message = () =>
            messageLogger.WasCalled(l => l.LogMessage(message));
    }

    [Subject(typeof(ConsoleBuildNotifier))]
    public class when_a_build_error_occurs : ConsoleBuildNotifierSpecs
    {
        const string error = "The build failed";

        Because of = () =>
            Subject.Error(error);

        It should_send_build_failed_message_to_console = () =>
                    console.WasCalled(l => l.Error(error));
    }

    [Subject(typeof(ConsoleBuildNotifier))]
    public class when_a_build_completes_with_success : ConsoleBuildNotifierSpecs
    {
        Because of = () =>
            Subject.BuildFinished();

        It should_send_build_succeeded_message_to_console = () =>
            console.WasCalled(l => l.Info("Build succeeded"));
    }

    public abstract class ConsoleBuildNotifierSpecs : Specification<ConsoleBuildNotifier>
    {
        protected static IConsole console;
        protected static IMessageLogger messageLogger;

        Establish context = () =>
        {
            console = Mock<IConsole>();
            messageLogger = Mock<IMessageLogger>();
            Subject = new ConsoleBuildNotifier(console, messageLogger);
        };
    }
}