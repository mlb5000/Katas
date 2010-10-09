using System;

namespace AutoBuild.Core
{
    public interface IConsole
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Error(string message, Exception exception);
    }
}