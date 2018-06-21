using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Matches;

namespace BetsKing.Server.Mappings
{
    public class MatchBetProfile : Profile
    {
        public MatchBetProfile()
        {
            CreateMap<MatchBet, MatchBetViewModel>()
                .ForMember(dest => dest.TeamAScoreString, opt => opt.MapFrom(src => src.TeamAScore.HasValue ? src.TeamAScore.Value.ToString() : null))
                .ForMember(dest => dest.TeamBScoreString, opt => opt.MapFrom(src => src.TeamBScore.HasValue ? src.TeamBScore.Value.ToString() : null))
                .ForMember(dest => dest.TeamAName, opt => opt.MapFrom(src => src.Match.TeamAName))
                .ForMember(dest => dest.TeamBName, opt => opt.MapFrom(src => src.Match.TeamBName))
                .ForMember(dest => dest.TeamAScoreBet, opt => opt.MapFrom(src => src.TeamAScore))
                .ForMember(dest => dest.TeamBScoreBet, opt => opt.MapFrom(src => src.TeamBScore))
                .ForMember(dest => dest.RoundId, opt => opt.MapFrom(src => src.Match.RoundId))
                .ForMember(dest => dest.RoundName, opt => opt.MapFrom(src => src.Match.Round.Name));

            CreateMap<MatchBetViewModel, UpdateMatchBetViewModel>();
        }
    }
}
