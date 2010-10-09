using System;

namespace AutoBuild.Core
{
    public interface IBuildRunner
    {
        void RunBuild();
        event EventHandler<EventArgs> BuildStarted;
        event EventHandler<EventArgs> BuildCompleted;
    }
}