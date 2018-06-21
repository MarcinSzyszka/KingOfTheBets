using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Services.Tournaments;
using BetsKing.Shared.ViewModels.Tournaments;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetsKing.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TournamentController : Controller
    {
        readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tournaments = _tournamentService.GetAll().ToList();

            return Json(Mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentViewModel>>(tournaments));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tournament = await _tournamentService.Get(id);

            return Json(Mapper.Map<Tournament, TournamentViewModel>(tournament));
        }

        [HttpGet("GetForGambler/{id}")]
        public async Task<IActionResult> GetForGambler(int id)
        {
            var tournaments = await _tournamentService.GetForGambler(id);

            return Json(Mapper.Map<IEnumerable<Tournament>, IEnumerable<TournamentViewModel>>(tournaments));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            var tournament = await _tournamentService.Create(name);

            return Json(tournament.Id);
        }


        [HttpGet("getallgamblers/{id}")]
        public async Task<IActionResult> GetAllGamblers(int id)
        {
            var tournamentGamblers = await _tournamentService.GetAllGamblers(id);

            return Json(tournamentGamblers);
        }

        
        [HttpPut("setgamblers")]
        public async Task<IActionResult> SetGamblers([FromBody] SetTournamentGamblersViewModel model)
        {
            var result = await _tournamentService.SetGamblers(model);

            return Json(result);
        }

        
    }
}