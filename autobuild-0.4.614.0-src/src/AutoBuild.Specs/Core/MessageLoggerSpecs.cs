using AutoBuild.Core;
using Machine.Specifications;

namespace AutoBuild.Specs.Core
{
    [Subject(typeof(MessageLogger))]
    public class when_logging_a_message : MessageLoggerSpecs
    {
        const string message = "test message";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_debug_message_to_console = () =>
            console.WasCalled(l => l.Debug(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_a_message_with_leading_whitespace : MessageLoggerSpecs
    {
        Because of = () =>
            Subject.LogMessage("    Tests Run: 12, Failures: 0");

        It should_send_trimmed_message_to_console = () =>
            console.WasCalled(l => l.Debug("Tests Run: 12, Failures: 0"));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_nunit_test_run_failure_message : MessageLoggerSpecs
    {
        private const string message = "Tests Run: 12, Failures: 5";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_error_message_to_console = () =>
            console.WasCalled(l => l.Error(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_mspec_test_failure_message : MessageLoggerSpecs
    {
        private const string message = ">> should send error message to console (FAIL)";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_error_message_to_console = () =>
            console.WasCalled(l => l.Error(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_mspec_test_run_failure_message : MessageLoggerSpecs
    {
        private const string message = "10 passed, 3 failed";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_error_message_to_console = () =>
            console.WasCalled(l => l.Error(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_nunit_test_run_success_message : MessageLoggerSpecs
    {
        private const string message = "Tests Run: 12, Failures: 0";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_info_message_to_console = () =>
            console.WasCalled(l => l.Info(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_a_compiler_error : MessageLoggerSpecs
    {
        private const string message = "error CS1002: ; expected";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_error_message_to_console = () =>
            console.WasCalled(l => l.Error(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_logging_an_msbuild_error : MessageLoggerSpecs
    {
        private const string message = "error MSB1009: Project file does not exist";

        Because of = () =>
            Subject.LogMessage(message);

        It should_send_error_message_to_console = () =>
            console.WasCalled(l => l.Error(message));
    }

    [Subject(typeof(MessageLogger))]
    public class when_same_message_is_logged_more_than_once : MessageLoggerSpecs
    {
        private const string message = "Program.cs(3,21): error CS1002: ; expected";

        Because of = () =>
        {
            Subject.LogMessage(message);
            Subject.LogMessage(message);
        };

        It should_only_send_message_to_console_once = () =>
            console.WasCalled(l => l.Error(message), c => c.Repeat.Once());
    }

    [Subject(typeof(MessageLogger))]
    public class when_clearing_the_message_log : MessageLoggerSpecs
    {
        Because of = () =>
            Subject.ClearLog();

        It should_clear_the_message_log = () =>
            Subject.LoggedMessages.ShouldBeEmpty();
    }

    public abstract class MessageLoggerSpecs : Specification<MessageLogger>
    {
        protected static IConsole console;

        Establish context = () =>
        {
            console = Mock<IConsole>();
            Subject = new MessageLogger(console);
        };
    }
}