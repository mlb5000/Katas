using NUnit.Framework;

namespace TennisKata.Tests.Unit
{
    [TestFixture]
    public class TennisKataTests
    {
        private TennisMatch match;

        [TestCase(0, 0, SideScored.None, "Left: love Right: love")]
        [TestCase(0, 0, SideScored.Left, "Left: fifteen Right: love")]
        [TestCase(1, 0, SideScored.Left, "Left: thirty Right: love")]
        [TestCase(2, 0, SideScored.Left, "Left: forty Right: love")]
        [TestCase(0, 0, SideScored.Right, "Left: love Right: fifteen")]
        [TestCase(0, 1, SideScored.Right, "Left: love Right: thirty")]
        [TestCase(0, 2, SideScored.Right, "Left: love Right: forty")]
        [TestCase(3, 2, SideScored.Right, "deuce")]
        [TestCase(2, 1, SideScored.Right, "Left: thirty Right: thirty")]
        [TestCase(4, 3, SideScored.Right, "deuce")]
        [TestCase(3, 3, SideScored.Right, "Advantage Right")]
        [TestCase(4, 4, SideScored.Right, "Advantage Right")]
        [TestCase(3, 3, SideScored.Left, "Advantage Left")]
        [TestCase(4, 4, SideScored.Left, "Advantage Left")]
        public void TennisKataScoreTests(int initLeft, int initRight, SideScored sideScored, string expectedScore)
        {
            GivenMatchWithCurrentScore(initLeft, initRight);

            WhenSideScores(sideScored);

            ThenScoreboardShouldRead(expectedScore);
        }

        private void GivenMatchWithCurrentScore(int left, int right)
        {
            match = new TennisMatch();
            for (int i = 0; i < left; i++)
                match.BallWon(SideScored.Left);
            for (int i = 0; i < right; i++)
                match.BallWon(SideScored.Right);
        }

        private void WhenSideScores(SideScored scored)
        {
            match.BallWon(scored);
        }

        private void ThenScoreboardShouldRead(string score)
        {
            Assert.AreEqual(score, match.Scoreboard());
        }

        [TestCase(4, 3, SideScored.Left, ExpectedException = typeof (GameOverException), ExpectedMessage = "Left WINS!")
        ]
        [TestCase(5, 4, SideScored.Left, ExpectedException = typeof (GameOverException), ExpectedMessage = "Left WINS!")
        ]
        [TestCase(4, 5, SideScored.Right, ExpectedException = typeof (GameOverException),
            ExpectedMessage = "Right WINS!")]
        [TestCase(124, 125, SideScored.Right, ExpectedException = typeof (GameOverException),
            ExpectedMessage = "Right WINS!")]
        public void BallWon_CausesGameOver_ThrowsException(int initLeft, int initRight, SideScored sideScored)
        {
            GivenMatchWithCurrentScore(initLeft, initRight);

            WhenSideScores(sideScored);

            ThenCheckingForWinnerShouldThrowException();
        }

        private void ThenCheckingForWinnerShouldThrowException()
        {
            match.Scoreboard();
        }
    }
}