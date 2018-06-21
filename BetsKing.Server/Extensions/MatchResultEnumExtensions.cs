using BetsKing.Shared.Attributes;
using BetsKing.Shared.Enums;
using System;
using System.Linq;

namespace BetsKing.Server.Extensions
{
    public static class MatchResultEnumExtensions
    {
        public static int GetResultScore(this MatchResultEnum enumValue)
        {
            var type = enumValue.GetType();
            var name = Enum.GetName(type, enumValue);

            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<ResultScoreAttribute>()
                .SingleOrDefault()?.Score ?? 0;
        }
    }
}
