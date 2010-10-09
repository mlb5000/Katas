using AutoBuild.Core;
using Machine.Specifications;

namespace AutoBuild.Specs.Core
{
    [Subject(typeof(FileChangedFilter))]
    public class when_filtering_a_cs_file : FileChangedFilterSpecs
    {
        It should_allow_file = () =>
            Subject.IsMatch(csFile).ShouldBeTrue();
    }

    [Subject(typeof(FileChangedFilter))]
    public class when_filtering_a_csproj_file : FileChangedFilterSpecs
    {
        It should_allow_file = () =>
            Subject.IsMatch(csprojFile).ShouldBeTrue();
    }

    [Subject(typeof(FileChangedFilter))]
    public class when_filtering_a_vb_file : FileChangedFilterSpecs
    {
        It should_allow_file = () =>
            Subject.IsMatch(vbFile).ShouldBeTrue();
    }

    [Subject(typeof(FileChangedFilter))]
    public class when_filtering_a_vbproj_file : FileChangedFilterSpecs
    {
        It should_allow_file = () =>
            Subject.IsMatch(vbprojFile).ShouldBeTrue();
    }

    [Subject(typeof(FileChangedFilter))]
    public class when_filtering_a_non_code_file : FileChangedFilterSpecs
    {
        It should_not_allow_file = () =>
            Subject.IsMatch(nonCodeFile).ShouldBeFalse();
    }

    [Subject(typeof(FileChangedFilter))]
    public class when_filtering_a_tmp_file : FileChangedFilterSpecs
    {
        It should_not_allow_file = () =>
            Subject.IsMatch(tmpFile).ShouldBeFalse();
    }

    public abstract class FileChangedFilterSpecs : Specification<FileChangedFilter>
    {
        protected static string csFile = @"c:\temp\test.cs";
        protected static string csprojFile = @"c:\temp\test.csproj";
        protected static string vbFile = @"c:\temp\test.vb";
        protected static string vbprojFile = @"c:\temp\test.vbproj";
        protected static string nonCodeFile = @"c:\temp\test.txt";
        protected static string tmpFile = @"AutoBuild.Tests\Core\ve-F030.tmp";

        Establish context = () =>
            Subject = new FileChangedFilter();
    }
}
