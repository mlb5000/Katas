using Rhino.Mocks;

namespace AutoBuild.Specs
{
    public class Specification<TSubject>
    {
        protected static TSubject Subject { get; set; }

        protected static T Mock<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }
    }
}