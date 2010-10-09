using NUnit.Framework;

namespace KataTennis.Tests.Unit
{
    [TestFixture]
    public class TennisScoreTests
    {
        private Match match;

        [SetUp]
        public void SetUp()
        {
            match = new Match();
        }

        [TestCase]
        public void TestDefault()
        {
            Assert.AreEqual("Left: love Right: love", match.ScoreDescription());
        }

        [TestCase(0, 0, SideScored.Left, "Left: fifteen Right: love")]
        [TestCase(1, 0, SideScored.Left, "Left: thirty Right: love")]
        [TestCase(2, 0, SideScored.Left, "Left: forty Right: love")]
        [TestCase(3, 0, SideScored.Left, "Left: WINNER! Right: love")]
        [TestCase(0, 3, SideScored.Right, "Left: love Right: WINNER!")]
        [TestCase(1, 0, SideScored.Right, "Left: fifteen Right: fifteen")]
        [TestCase(2, 1, SideScored.Right, "Left: thirty Right: thirty")]
        [TestCase(3, 2, SideScored.Right, "deuce")]
        [TestCase(3, 3, SideScored.Right, "advantage Right")]
        [TestCase(3, 3, SideScored.Left, "advantage Left")]
        [TestCase(16, 15, SideScored.Right, "deuce")]
        [TestCase(32, 32, SideScored.Right, "advantage Right")]
        [TestCase(32, 32, SideScored.Left, "advantage Left")]
        [TestCase(33, 32, SideScored.Left, "Left: WINNER! Right: forty")]
        [TestCase(32, 33, SideScored.Right, "Left: forty Right: WINNER!")]
        public void TestScored(
            int initLeft, 
            int initRight, 
            SideScored scored, 
            string matchScore)
        {
            match.Left.Score.Value = initLeft;
            match.Right.Score.Value = initRight;

            match.Scored(scored);

            Assert.AreEqual(matchScore, match.ScoreDescription());
        }
    }
}