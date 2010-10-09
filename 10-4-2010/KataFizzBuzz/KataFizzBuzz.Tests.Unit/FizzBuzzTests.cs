using NUnit.Framework;

namespace KataFizzBuzz.Tests.Unit
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [TestCase(1, "1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void TestFizzBuzz(int number, string expected)
        {
            string result = FizzBuzz.FizzOrBuzz(number);

            Assert.AreEqual(expected, result);
        }
    }
}