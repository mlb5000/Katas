using System;
using AutoBuild.Console;
using AutoBuild.Core.BuildNotification;
using Machine.Specifications;
using NAnt.Core;

namespace AutoBuild.Specs.Console
{
    [Subject(typeof(NAntBuildLogger))]
    public class when_build_is_started : NAntBuildLoggerSpecs
    {
        Because of = () =>
            Subject.BuildStarted(null, new BuildEventArgs());

        It should_inform_the_notification_system = () =>
            notificationSystem.WasCalled(n => n.BuildStarted());
    }

    [Subject(typeof(NAntBuildLogger))]
    public class when_build_message_is_received : NAntBuildLoggerSpecs
    {
        Because of = () =>
            Subject.MessageLogged(null, buildEventArgs);

        It should_inform_the_notification_system = () =>
            notificationSystem.WasCalled(n => n.MessageLogged(buildEventArgs.Message));
    }

    [Subject(typeof (NAntBuildLogger))]
    public class when_build_completes_with_errors : NAntBuildLoggerSpecs
    {
        Establish context = () =>
        {
            buildEventArgs.Exception = new Exception();
            buildEventArgs.Message = "error";
        };

        Because of = () =>
            Subject.BuildFinished(null, buildEventArgs);

        It should_inform_the_notification_system = () =>
            notificationSystem.WasCalled(n => n.Error("Build failed"));
    }

    [Subject(typeof(NAntBuildLogger))]
    public class when_build_completes_with_success : NAntBuildLoggerSpecs
    {
        Because of = () =>
            Subject.BuildFinished(null, buildEventArgs);

        It should_inform_the_notification_system = () =>
            notificationSystem.WasCalled(n => n.BuildFinished());
    }

    public abstract class NAntBuildLoggerSpecs : Specification<NAntBuildLogger>
    {
        protected static IBuildNotificationSystem notificationSystem;
        protected static BuildEventArgs buildEventArgs;

        Establish context = () =>
        {
            notificationSystem = Mock<IBuildNotificationSystem>();
            buildEventArgs = new BuildEventArgs();
            Subject = new NAntBuildLogger(notificationSystem);
        };
    }
}