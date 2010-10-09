using NUnit.Framework;

namespace TennisKata.Tests.Unit
{
    [TestFixture]
    public class TennisUnitTests
    {
        private Match _match;

        [TestCase(0, 0, SideScored.None, "Left: love Right: love")]
        [TestCase(0, 0, SideScored.Left, "Left: fifteen Right: love")]
        [TestCase(1, 0, SideScored.Left, "Left: thirty Right: love")]
        [TestCase(2, 0, SideScored.Left, "Left: forty Right: love")]
        [TestCase(0, 0, SideScored.Right, "Left: love Right: fifteen")]
        [TestCase(0, 1, SideScored.Right, "Left: love Right: thirty")]
        [TestCase(0, 2, SideScored.Right, "Left: love Right: forty")]
        [TestCase(1, 2, SideScored.Left, "Left: thirty Right: thirty")]
        [TestCase(2, 3, SideScored.Left, "deuce")]
        [TestCase(3, 4, SideScored.Left, "deuce")]
        [TestCase(3, 2, SideScored.Right, "deuce")]
        [TestCase(125, 126, SideScored.Left, "deuce")]
        [TestCase(126, 125, SideScored.Right, "deuce")]
        [TestCase(3, 3, SideScored.Left, "Advantage Left")]
        [TestCase(3, 3, SideScored.Right, "Advantage Right")]
        [TestCase(126, 126, SideScored.Right, "Advantage Right")]
        [TestCase(126, 126, SideScored.Left, "Advantage Left")]
        [TestCase(0, 3, SideScored.Right, "Right WINS!")]
        [TestCase(3, 0, SideScored.Left, "Left WINS!")]
        [TestCase(4, 3, SideScored.Left, "Left WINS!")]
        [TestCase(3, 4, SideScored.Right, "Right WINS!")]
        [TestCase(5, 4, SideScored.Left, "Left WINS!")]
        [TestCase(6, 5, SideScored.Left, "Left WINS!")]
        [TestCase(5, 6, SideScored.Right, "Right WINS!")]
        public void TennisTests(int leftScore, int rightScore, SideScored sideScored, string expectedScore)
        {
            GivenMatchWithCurrentScore(leftScore, rightScore);

            WhenSideScores(sideScored);

            ThenScoreboardShouldRead(expectedScore);
        }

        private void ThenScoreboardShouldRead(string expectedScore)
        {
            Assert.AreEqual(expectedScore, _match.Scoreboard());
        }

        private void WhenSideScores(SideScored scored)
        {
            _match.SideWonBall(scored);
        }

        private void GivenMatchWithCurrentScore(int leftScore, int rightScore)
        {
            _match = new Match();
            for (var i = 0; i < leftScore; i++)
                _match.LeftSide.WonBall();
            for (var i = 0; i < rightScore; i++)
                _match.RightSide.WonBall();
        }
    }
}