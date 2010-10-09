using System;
using System.Collections.Generic;
using System.Linq;
using AutoBuild.Core.BuildNotification;
using Machine.Specifications;

namespace AutoBuild.Specs.Core.BuildNotification
{
    [Subject(typeof(BuildNotificationSystem))]
    public class when_a_build_starts : BuildNotificationSystemSpecs
    {
        Because of = () =>
            Subject.BuildStarted();

        It should_notify_each_build_notifier = () =>
            buildNotifiers.ShouldReceive(n => n.BuildStarted());
    }

    [Subject(typeof(BuildNotificationSystem))]
    public class when_a_build_message_is_received : BuildNotificationSystemSpecs
    {
        const string message = "A message recieved from the build";

        Because of = () =>
            Subject.MessageLogged(message);

        It should_notify_each_build_notifier = () =>
            buildNotifiers.ShouldReceive(n => n.MessageLogged(message));
    }

    [Subject(typeof(BuildNotificationSystem))]
    public class when_a_build_error_is_received : BuildNotificationSystemSpecs
    {
        const string error = "A build error";

        Because of = () =>
            Subject.Error(error);

        It should_notify_each_build_notifier = () =>
            buildNotifiers.ShouldReceive(n => n.Error(error));
    }

    [Subject(typeof(BuildNotificationSystem))]
    public class when_a_build_finishes : BuildNotificationSystemSpecs
    {
        Because of = () =>
            Subject.BuildFinished();

        It should_notify_each_build_notifier = () =>
            buildNotifiers.ShouldReceive(n => n.BuildFinished());
    }

    public abstract class BuildNotificationSystemSpecs : Specification<BuildNotificationSystem>
    {
        protected static IBuildNotifier[] buildNotifiers;

        Establish context = () =>
        {
            buildNotifiers = FiveMockBuildNotifiers().ToArray();
            Subject = new BuildNotificationSystem(buildNotifiers);
        };

        static IEnumerable<IBuildNotifier> FiveMockBuildNotifiers()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return Mock<IBuildNotifier>();
            }
        }
    }

    public static class BuildNotificationSystemSpecsExtensions
    {
        public static void ShouldReceive(this IEnumerable<IBuildNotifier> buildNotifiers,
            Action<IBuildNotifier> action)
        {
            foreach (IBuildNotifier buildNotifier in buildNotifiers)
            {
                buildNotifier.WasCalled(action);
            }
        }
    }
}