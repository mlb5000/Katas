using System.Collections.Generic;
using System.Linq;

namespace AutoBuild.Core
{
    public class MessageLogger : IMessageLogger
    {
        private readonly IConsole console;
        private readonly IList<string> loggedMessages = new List<string>();

        public MessageLogger(IConsole console)
        {
            this.console = console;
        }

        public string[] LoggedMessages
        {
            get { return loggedMessages.ToArray(); }
        }

        public void ClearLog()
        {
            loggedMessages.Clear();
        }

        public void LogMessage(string message)
        {
            message = message.Trim();

            if (loggedMessages.Contains(message))
                return;

            LogDebug(message);
            LogBuildError(message);
            LogTestFailure(message);
            LogTestSuccess(message);

            loggedMessages.Add(message);
        }

        private void LogDebug(string message)
        {
            console.Debug(message);
        }

        private void LogTestFailure(string message)
        {
            if (NUnitTestRunFailure(message)
                || MSpecTestFailure(message)
                || MSpecTestRunFailure(message))
            {
                console.Error(message);
            }
        }

        private void LogTestSuccess(string message)
        {
            if (NUnitTestRunSuccess(message))
            {
                console.Info(message);
            }
        }

        private void LogBuildError(string message)
        {
            if (MSBuildError(message)
                || CompilerError(message))
            {
                console.Error(message);
            }
        }

        private bool CompilerError(string message)
        {
            return message.Matches(@"error CS\d{4}:");
        }

        private bool MSBuildError(string message)
        {
            return message.Matches(@"error MSB\d{4}:");
        }

        private bool NUnitTestRunFailure(string message)
        {
            return message.Matches(@"Failures: [^0]");
        }

        private bool MSpecTestFailure(string message)
        {
            return message.Matches(@">>.+(FAIL)");
        }

        private bool MSpecTestRunFailure(string message)
        {
            return message.Matches(@"\d+ passed, [^0] failed");
        }

        private bool NUnitTestRunSuccess(string message)
        {
            return message.Matches(@"Failures: 0");
        }
    }
}