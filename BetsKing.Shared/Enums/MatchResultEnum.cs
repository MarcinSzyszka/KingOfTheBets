using BetsKing.Shared.Attributes;

namespace BetsKing.Shared.Enums
{
    public enum MatchResultEnum
    {
        [ResultScore(0)]
        Pending,
        [ResultScore(1)]
        I,
        [ResultScore(1)]
        X,
        [ResultScore(1)]
        II
    }
}
