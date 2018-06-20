using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;

namespace BetsKing.Server.Mappings
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, MatchViewModel>()
                .ForMember(dest => dest.TeamAScoreString, opt => opt.MapFrom(src => src.TeamAScore.HasValue ? src.TeamAScore.Value.ToString() : null))
                .ForMember(dest => dest.TeamBScoreString, opt => opt.MapFrom(src => src.TeamBScore.HasValue ? src.TeamBScore.Value.ToString() : null));
        }
    }
}
