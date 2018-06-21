using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Gambler;
using BetsKing.Shared.ViewModels.Tournaments;
using System.Linq;

namespace BetsKing.Server.Mappings
{
    public class GamblerProfile : Profile
    {
        public GamblerProfile()
        {
            CreateMap<Gambler, GamblerViewModel>()
                .ForMember(g => g.IsAdmin, opt => opt.MapFrom(src => src.Roles.Any(r => r.Role.Name == "Admin")))
                .ForMember(g => g.Roles, opt => opt.MapFrom(src => string.Join(',', src.Roles.Select(r => r.Role).Select(r => r.Name))));

            CreateMap<Gambler, TournamentGamblerViewModel>()
                .ForMember(dest => dest.GamblerId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
