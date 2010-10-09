using System;

namespace KataTennis
{
    public enum SideScored
    {
        Left,
        Right
    }

    public class Match
    {
        public Match()
        {
            Left = new Side();
            Right = new Side();
        }

        public Side Left { get; set; }
        public Side Right { get; set; }

        public void Scored(SideScored scored)
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

        public string ScoreDescription()
        {
            if (IsDeuce())
            {
                return "deuce";
            }
            if (IsAdvantage())
            {
                return AdvantageString();
            }
            return WinnerString();
        }

        private string AdvantageString()
        {
            return string.Format("advantage {0}", Left.Score.Value > Right.Score.Value ? "Left" : "Right");
        }

        private bool IsAdvantage()
        {
            return (Left.Score.Value >= 3 && Right.Score.Value >= 3) && !IsDeuce() && !SomeoneWon();
        }

        private string WinnerString()
        {
            string left = LeftWon() ? "WINNER!" : Left.Score.ValueString();
            string right = RightWon() ? "WINNER!" : Right.Score.ValueString();

            return string.Format("Left: {0} Right: {1}", left, right);
        }

        private bool IsDeuce()
        {
            return (Left.Score.Value >= 3 && Right.Score.Value >= 3) && Left.Score.Value == Right.Score.Value;
        }

        private bool SomeoneWon()
        {
            return LeftWon() || RightWon();
        }

        private bool RightWon()
        {
            return (Right.Score.Value >= 4) && ((Right.Score.Value - Left.Score.Value) >= 2);
        }

        private bool LeftWon()
        {
            return (Left.Score.Value >= 4) && ((Left.Score.Value - Right.Score.Value) >= 2);
        }
    }

    public class Side
    {
        public Side()
        {
            Score = new Score();
        }

        public void Scored()
        {
            Score.Value++;
        }

        public Score Score { get; set; }
    }

    public class Score
    {
        public Score()
        {
            Value = 0;
        }

        public int Value { get; set; }

        public string ValueString()
        {
            if (Value == 0)
            {
                return "love";
            }
            else if (Value == 1)
            {
                return "fifteen";
            }
            else if (Value == 2)
            {
                return "thirty";
            }

            return "forty";
        }
    }
}