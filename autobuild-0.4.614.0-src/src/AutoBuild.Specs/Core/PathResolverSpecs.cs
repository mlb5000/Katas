using System;
using System.IO;
using System.Reflection;
using AutoBuild.Core;
using Machine.Specifications;

namespace AutoBuild.Specs.Core
{
    [Subject(typeof(PathResolver))]
    public class when_resolving_an_absolute_path : PathResolverSpecs
    {
        It should_return_absolute_path = () =>
            Path.IsPathRooted(Subject.Resolve(absolute_path)).ShouldBeTrue();
    }

    [Subject(typeof(PathResolver))]
    public class when_resolving_a_relative_path : PathResolverSpecs
    {
        It should_return_absolute_path = () =>
            Path.IsPathRooted(Subject.Resolve(relative_path)).ShouldBeTrue();
    }

    public abstract class PathResolverSpecs : Specification<PathResolver>
    {
        protected static string relative_path = @".\AutoBuild.Specs.dll";
        protected static string absolute_path = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;

        Establish context = () =>
            Subject = new PathResolver();
    }
}