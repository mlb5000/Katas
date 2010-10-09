using System;
using AutoBuild.Core;

namespace AutoBuild.Console
{
    public class EmptyExecutionArguments : ExecutionArguments
    {
        public EmptyExecutionArguments() : base(String.Empty, String.Empty)
        {
        }
    }
}