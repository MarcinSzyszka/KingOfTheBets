using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Tournaments;

namespace BetsKing.Server.Mappings
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentViewModel>();
        }
    }
}
