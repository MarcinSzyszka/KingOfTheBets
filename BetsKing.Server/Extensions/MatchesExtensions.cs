using BetsKing.Server.Data.Entity;
using BetsKing.Shared.Enums;

namespace BetsKing.Server.Extensions
{
    public static class MatchesExtensions
    {
        public static MatchResultEnum GetResult(this MatchBase match)
        {
            if (!match.TeamAScore.HasValue)
            {
                return MatchResultEnum.Pending;
            }

            if (match.TeamAScore.Value > match.TeamBScore.Value)
            {
                return MatchResultEnum.I;
            }

            if (match.TeamAScore.Value < match.TeamBScore.Value)
            {
                return MatchResultEnum.II;
            }

            return MatchResultEnum.X;
        }
    }
}
