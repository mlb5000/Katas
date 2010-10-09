using System.IO;
using AutoBuild.Core;
using AutoBuild.Core.BuildNotification;
using Castle.Windsor;
using log4net.Config;
using NAnt.Core;

namespace AutoBuild.Console
{
    public class NAntBuildLogger : IBuildLogger
    {
        private readonly IBuildNotificationSystem buildNotificationSystem;

        public NAntBuildLogger()
        {
            string configFilePath = new PathResolver().Resolve(@".\AutoBuild.Console.exe.config");
            FileStream configFile = File.OpenRead(configFilePath);
            XmlConfigurator.Configure(configFile);

            var container = new WindsorContainer();

            var configuration = new WindsorConfiguration(container);
            configuration.ConfigureContainer();

            buildNotificationSystem = container.Resolve<IBuildNotificationSystem>();
        }

        public NAntBuildLogger(IBuildNotificationSystem buildNotificationSystem)
        {
            this.buildNotificationSystem = buildNotificationSystem;
        }

        public void BuildStarted(object sender, BuildEventArgs e)
        {
            buildNotificationSystem.BuildStarted();
        }

        public void BuildFinished(object sender, BuildEventArgs e)
        {
            if (e.Exception != null)
            {
                buildNotificationSystem.Error("Build failed");
            }
            else
            {
                buildNotificationSystem.BuildFinished();
            }
        }

        public void TargetStarted(object sender, BuildEventArgs e)
        {
        }

        public void TargetFinished(object sender, BuildEventArgs e)
        {
        }

        public void TaskStarted(object sender, BuildEventArgs e)
        {
        }

        public void TaskFinished(object sender, BuildEventArgs e)
        {
        }

        public void MessageLogged(object sender, BuildEventArgs e)
        {
            buildNotificationSystem.MessageLogged(e.Message);
        }

        public void Flush()
        {
        }

        public Level Threshold { get; set; }

        public bool EmacsMode { get; set; }

        public TextWriter OutputWriter { get; set; }
    }
}