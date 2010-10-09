using System;

namespace TennisKata
{
    public enum SideScored
    {
        Left,
        Right,
        None
    }

    public class Match
    {
        public Match()
        {
            LeftSide = new Side();
            RightSide = new Side();
        }

        public Side LeftSide { get; set; }
        public Side RightSide { get; set; }

        public void SideWonBall(SideScored scored)
        {
            if (scored == SideScored.Left)
                LeftSide.WonBall();
            else if (scored == SideScored.Right)
                RightSide.WonBall();
        }

        public string Scoreboard()
        {
            if (LeftSide.AtDeuceWith(RightSide))
                return "deuce";
            if (LeftSide.HasAdvantageOver(RightSide))
                return "Advantage Left";
            if (RightSide.HasAdvantageOver(LeftSide))
                return "Advantage Right";
            if (RightSide.HasBeaten(LeftSide))
                return "Right WINS!";
            if (LeftSide.HasBeaten(RightSide))
                return "Left WINS!";
            return String.Format("Left: {0} Right: {1}",
                                 LeftSide.CurrentScore(),
                                 RightSide.CurrentScore());
        }
    }

    public class Side
    {
        public Side()
        {
            Score = new Score();
        }

        private Score Score { get; set; }

        public void WonBall()
        {
            Score.AwardPoint();
        }

        public bool AtDeuceWith(Side otherSide)
        {
            return EligibleForDeuceWith(otherSide) && Score.Equals(otherSide.Score);
        }

        private bool EligibleForDeuceWith(Side otherSide)
        {
            return Score.BallsWon >= 3 && otherSide.Score.BallsWon >= 3;
        }

        public bool HasAdvantageOver(Side otherSide)
        {
            return EligibleForDeuceWith(otherSide) &&
                   PointsAheadOf(otherSide) == 1;
        }

        private int PointsAheadOf(Side otherSide)
        {
            return (Score.BallsWon - otherSide.Score.BallsWon);
        }

        public bool HasBeaten(Side otherSide)
        {
            return Score.BallsWon > 3 && PointsAheadOf(otherSide) >= 2;
        }

        public string CurrentScore()
        {
            return Score.ToString();
        }
    }

    internal class Score
    {
        private int _ballsWon;
        public int BallsWon
        {
            get { return _ballsWon; }
        }

        public Score()
        {
            _ballsWon = 0;
        }

        public void AwardPoint()
        {
            _ballsWon++;
        }

        public override string ToString()
        {
            if (BallsWon == 0)
                return "love";
            if (BallsWon == 1)
                return "fifteen";
            if (BallsWon == 2)
                return "thirty";

            return "forty";
        }

        public override bool Equals(object obj)
        {
            return BallsWon == ((Score) obj).BallsWon;
        }
    }
}