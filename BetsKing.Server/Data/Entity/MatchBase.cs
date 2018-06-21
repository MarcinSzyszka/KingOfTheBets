using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetsKing.Server.Data.Entity
{
    public abstract class MatchBase : BaseEntity
    {
        public int? TeamAScore { get; set; } = null;

        public int? TeamBScore { get; set; } = null;
    }
}
