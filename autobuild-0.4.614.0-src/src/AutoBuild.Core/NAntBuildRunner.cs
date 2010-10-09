using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AutoBuild.Core
{
    public class NAntBuildRunner : IBuildRunner
    {
        private readonly string buildFile;
        private readonly string nantExePath;
        private readonly IConsole console;

        public event EventHandler<EventArgs> BuildStarted = delegate { };
        public event EventHandler<EventArgs> BuildCompleted = delegate { };

        public bool CreateNoWindow { get; set; }

        public NAntBuildRunner(ExecutionArguments arguments, IConfigurationSettings configurationSettings,
                               IPathResolver pathResolver, IConsole console)
        {
            buildFile = arguments.BuildFile;
            this.console = console;
            nantExePath = pathResolver.Resolve(configurationSettings.NAntExePath);

            console.Debug("NAnt executable path, {0}".With(nantExePath));
        }

        public void RunBuild()
        {
            OnBuildStarted();
            ExecuteBuildProcess();
            OnBuildCompleted();
        }

        private void ExecuteBuildProcess()
        {
            try
            {
                Process process = CreateBuildProcess();
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                console.Error("Error starting build process", ex);
            }
        }

        private Process CreateBuildProcess()
        {
            return new Process
                       {
                           StartInfo =
                               {
                                   FileName = nantExePath,
                                   Arguments =
                                       "-f:{0} -ext:\"{1}\" -logger:AutoBuild.Console.NAntBuildLogger -e -nologo"
                                       .With(buildFile, Path.Combine(Path.GetDirectoryName(
                                                                         new Uri(
                                                                             Assembly.GetExecutingAssembly().CodeBase).
                                                                             LocalPath),
                                                                     "AutoBuild.Console.exe")),
                                   UseShellExecute = false,
                                   CreateNoWindow = CreateNoWindow
                               }
                       };
        }

        private void OnBuildStarted()
        {
            BuildStarted(this, EventArgs.Empty);
        }

        private void OnBuildCompleted()
        {
            BuildCompleted(this, EventArgs.Empty);
        }
    }
}