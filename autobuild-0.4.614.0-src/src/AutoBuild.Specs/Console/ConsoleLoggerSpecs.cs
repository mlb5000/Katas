using System;
using AutoBuild.Core;
using log4net;
using Machine.Specifications;

namespace AutoBuild.Specs.Console
{
    [Subject(typeof(ConsoleLogger))]
    public class when_receiving_a_debug_message : ConsoleLoggerSpecs
    {
        Because of = () =>
            Subject.Debug(message);

        It should_log_debug_message = () =>
            logger.WasCalled(l => l.Debug(message));
    }

    [Subject(typeof(ConsoleLogger))]
    public class when_receiving_an_info_message : ConsoleLoggerSpecs
    {
        Because of = () =>
            Subject.Info(message);

        It should_log_debug_message = () =>
            logger.WasCalled(l => l.Info(message));
    }

    [Subject(typeof(ConsoleLogger))]
    public class when_receiving_an_error_message : ConsoleLoggerSpecs
    {
        Because of = () =>
            Subject.Error(message);

        It should_log_debug_message = () =>
            logger.WasCalled(l => l.Error(message));
    }

    [Subject(typeof(ConsoleLogger))]
    public class when_receiving_an_error_message_with_an_exception : ConsoleLoggerSpecs
    {
        static readonly Exception exception = new Exception();

        Because of = () =>
            Subject.Error(message, exception);

        It should_log_error_message = () =>
            logger.WasCalled(l => l.Error(message, exception));
    }

    public abstract class ConsoleLoggerSpecs : Specification<ConsoleLogger>
    {
        protected static string message = "Message";
        protected static ILog logger;

        Establish context = () =>
        {
            logger = Mock<ILog>();
            Subject = new ConsoleLogger {Logger = logger};
        };
    }
}