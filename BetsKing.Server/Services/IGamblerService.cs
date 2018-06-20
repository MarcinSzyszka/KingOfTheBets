using System.Collections.Generic;
using System.Threading.Tasks;
using BetsKing.Server.Data.Entity;
using BetsKing.Shared.ViewModels.Auth;
using BetsKing.Shared.ViewModels.Gambler;

namespace BetsKing.Server.Services
{
    public interface IGamblerService
    {
        Task<string> Login(LoginViewModel model);
        Task<Gambler> GetGambler(int id);
        Task<IEnumerable<Gambler>> GetAll();
        Task<Gambler> Create(AddGamblerViewModel model);
    }
}