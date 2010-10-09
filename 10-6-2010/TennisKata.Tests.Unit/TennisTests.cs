using NUnit.Framework;

namespace TennisKata.Tests.Unit
{
    [TestFixture]
    internal class TennisTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            match = new Tennis();
        }

        #endregion

        private Tennis match;

        [TestCase(0, 0, SideScored.Left, "Left: fifteen Right: love")]
        [TestCase(0, 0, SideScored.Right, "Left: love Right: fifteen")]
        [TestCase(1, 0, SideScored.Left, "Left: thirty Right: love")]
        [TestCase(2, 0, SideScored.Left, "Left: forty Right: love")]
        [TestCase(3, 2, SideScored.Right, "deuce")]
        [TestCase(2, 3, SideScored.Left, "deuce")]
        [TestCase(3, 3, SideScored.Left, "Advantage Left")]
        [TestCase(3, 3, SideScored.Right, "Advantage Right")]
        [TestCase(3, 0, SideScored.Left, "Left WINS!")]
        [TestCase(0, 3, SideScored.Right, "Right WINS!")]
        [TestCase(4, 4, SideScored.Right, "Advantage Right")]
        [TestCase(4, 4, SideScored.Left, "Advantage Left")]
        [TestCase(827, 826, SideScored.Right, "deuce")]
        [TestCase(827, 827, SideScored.Right, "Advantage Right")]
        [TestCase(827, 827, SideScored.Left, "Advantage Left")]
        [TestCase(827, 826, SideScored.Left, "Left WINS!")]
        [TestCase(826, 827, SideScored.Right, "Right WINS!")]
        public void TestTennisScoring(int initLeft, int initRight, SideScored sideScored, string expected)
        {
            GivenTennisMatchWithCurrentScore(initLeft, initRight);

            WhenSideScores(sideScored);

            ThenScoreboardShouldRead(expected);
        }

        private void GivenTennisMatchWithCurrentScore(int left, int right)
        {
            match.Left.Score.Value = left;
            match.Right.Score.Value = right;
        }

        private void WhenSideScores(SideScored scored)
        {
            match.PointScored(scored);
        }

        private void ThenScoreboardShouldRead(string expected)
        {
            Assert.AreEqual(expected, match.Scoreboard());
        }
    }
}