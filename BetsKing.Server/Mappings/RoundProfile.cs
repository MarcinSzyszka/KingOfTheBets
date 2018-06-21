using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Rounds;

namespace BetsKing.Server.Mappings
{
    public class RoundProfile : Profile
    {
        public RoundProfile()
        {
            CreateMap<Round, RoundViewModel>();
            CreateMap<Round, RoundWithBetsViewModel>()
                .ForMember(dest => dest.MatchBets, opt => opt.Ignore());
        }
    }
}
