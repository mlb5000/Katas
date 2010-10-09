using System;

namespace TennisKata
{
    public enum SideScored
    {
        Left,
        Right
    }

    public class Tennis
    {
        public Tennis()
        {
            Left = new Side();
            Right = new Side();
        }

        public Side Left { get; set; }
        public Side Right { get; set; }

        public string Scoreboard()
        {
            if (Left.WonGameAgainst(Right))
                return "Left WINS!";
            if (Right.WonGameAgainst(Left))
                return "Right WINS!";
            if (Left.IsDeuceWith(Right))
                return "deuce";
            if (Left.HasAdvantageOver(Right))
                return "Advantage Left";
            if (Right.HasAdvantageOver(Left))
                return "Advantage Right";
            return String.Format("Left: {0} Right: {1}", Left.Score.Description(), Right.Score.Description());
        }

        public void PointScored(SideScored scored)
        {
            if (scored == SideScored.Left)
            {
                Left.Scored();
            }
            else
            {
                Right.Scored();
            }
        }
    }

    public class Side
    {
        public Side()
        {
            Score = new Score();
        }

        public Score Score { get; set; }

        public void Scored()
        {
            Score.Increment();
        }

        public bool IsDeuceWith(Side side)
        {
            return SidesEligibleForDeuce(side) && ScoresMatch(side);
        }

        private bool SidesEligibleForDeuce(Side side)
        {
            return (Score.Value >= 3 && side.Score.Value >= 3);
        }

        private bool ScoresMatch(Side side)
        {
            return (Score.Value == side.Score.Value);
        }

        public bool HasAdvantageOver(Side side)
        {
            return SidesEligibleForDeuce(side) && SideBeating(side);
        }

        private bool SideBeating(Side side)
        {
            return Score.Value > side.Score.Value;
        }

        public bool WonGameAgainst(Side side)
        {
            return Score.Value >= 4 && BeatingSideByAtLeastTwo(side);
        }

        private bool BeatingSideByAtLeastTwo(Side side)
        {
            return (Score.Value - side.Score.Value) >= 2;
        }
    }

    public class Score
    {
        public int Value { get; set; }

        public void Increment()
        {
            Value++;
        }

        public string Description()
        {
            if (Value == 0)
                return "love";
            if (Value == 1)
                return "fifteen";
            if (Value == 2)
                return "thirty";

            return "forty";
        }
    }
}