using System;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace AutoBuild.Specs
{
    public static class SpecificationExtensions
    {
        public static void WasCalled<T>(this T mockObject, Action<T> action)
        {
            mockObject.AssertWasCalled(action);
        }

        public static void WasCalled<T>(this T mockObject, Action<T> action,
                                         Action<IMethodOptions<object>> setupConstraints)
        {
            mockObject.AssertWasCalled(action, setupConstraints);
        }

        public static void WasNotCalled<T>(this T mockObject, Action<T> action)
        {
            mockObject.AssertWasNotCalled(action);
        }

        public static void WasNotCalled<T>(this T mockObject, Action<T> action,
                                            Action<IMethodOptions<object>> setupConstraints)
        {
            mockObject.AssertWasNotCalled(action, setupConstraints);
        }
    }
}