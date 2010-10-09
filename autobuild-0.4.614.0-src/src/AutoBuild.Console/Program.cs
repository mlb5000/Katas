using System;
using AutoBuild.Core;
using Castle.Windsor;
using log4net.Config;

namespace AutoBuild.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                RunApplication(args);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        private static void RunApplication(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Usage: AutoBuild.Console.exe [watch-path] [build-file]");

            string watchPath = args[0];
            string buildFile = args[1];

            XmlConfigurator.Configure();

            var container = new WindsorContainer();
            var executionArguments = new ExecutionArguments(buildFile, watchPath);

            var configuration = new WindsorConfiguration(container);
            configuration.ConfigureContainer(executionArguments);

            container.Resolve<AutoBuildRunner>().Execute();

            System.Console.Read();
        }
    }
}