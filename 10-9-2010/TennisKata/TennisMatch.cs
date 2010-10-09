using System;

namespace TennisKata
{
    public enum SideScored
    {
        Left,
        Right,
        None
    }

    public class GameOverException : Exception
    {
        private readonly string message;

        public GameOverException(string message)
        {
            this.message = message;
        }

        public override string Message
        {
            get { return message; }
        }
    }

    public class TennisMatch
    {
        public TennisMatch()
        {
            LeftSide = new Side();
            RightSide = new Side();
        }

        protected Side LeftSide { get; set; }
        protected Side RightSide { get; set; }

        public void BallWon(SideScored scored)
        {
            if (scored == SideScored.Left)
                LeftSide.AwardPoint();
            else if (scored == SideScored.Right)
                RightSide.AwardPoint();
        }

        public string Scoreboard()
        {
            CheckForWinner();

            if (LeftSide.AtDeuceWith(RightSide))
                return "deuce";
            if (LeftSide.HasAdvantageOver(RightSide))
                return "Advantage Left";
            if (RightSide.HasAdvantageOver(LeftSide))
                return "Advantage Right";
            return String.Format("Left: {0} Right: {1}", LeftSide.Score, RightSide.Score);
        }

        private void CheckForWinner()
        {
            if (LeftSide.HasBeaten(RightSide))
                throw new GameOverException("Left WINS!");
            if (RightSide.HasBeaten(LeftSide))
                throw new GameOverException("Right WINS!");
        }
    }

    public class Side
    {
        public Side()
        {
            Score = new TennisScore();
        }

        public TennisScore Score { get; set; }

        public void AwardPoint()
        {
            Score.Increment();
        }

        public bool AtDeuceWith(Side side)
        {
            return EligibleForDeuceWith(side) && Score.Equals(side.Score);
        }

        private bool EligibleForDeuceWith(Side side)
        {
            return Score.BallsWon >= 3 && side.Score.BallsWon >= 3;
        }

        public bool HasAdvantageOver(Side side)
        {
            return EligibleForDeuceWith(side) && !AtDeuceWith(side) && BeatingSideByOneBall(side);
        }

        private bool BeatingSideByOneBall(Side side)
        {
            return (Score.BallsWon - side.Score.BallsWon) == 1;
        }

        public bool HasBeaten(Side side)
        {
            return BeatingSideByAtLeastTwoBalls(side) && (Score.BallsWon >= 4);
        }

        private bool BeatingSideByAtLeastTwoBalls(Side side)
        {
            return Score.BallsWon - side.Score.BallsWon >= 2;
        }
    }

    public class TennisScore
    {
        public TennisScore()
        {
            BallsWon = 0;
        }

        public int BallsWon { get; set; }

        public void Increment()
        {
            BallsWon++;
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

        public bool Equals(TennisScore other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.BallsWon == BallsWon;
        }

        public override int GetHashCode()
        {
            return BallsWon;
        }
    }
}