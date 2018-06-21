using System;

namespace BetsKing.Shared.Attributes
{
    public class ResultScoreAttribute : Attribute
    {
        public ResultScoreAttribute(int score)
        {
            Score = score;
        }

        public int Score { get; }

    }
}
