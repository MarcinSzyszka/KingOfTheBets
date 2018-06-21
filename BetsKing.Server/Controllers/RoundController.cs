using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Services.Matches;
using BetsKing.Server.Services.Rounds;
using BetsKing.Shared.ViewModels.Matches;
using BetsKing.Shared.ViewModels.Rounds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetsKing.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoundController : Controller
    {
        readonly IRoundService _roundService;

        readonly IMatchBetService _matchBetService;

        public RoundController(IRoundService roundService, IMatchBetService matchBetService)
        {
            _roundService = roundService;
            _matchBetService = matchBetService;
        }

        [HttpGet("GetByTournament/{tournamentId}")]
        public async Task<IActionResult> GetByTournament(int tournamentId)
        {
            var tournaments = await _roundService.GetAll(tournamentId);

            return Json(Mapper.Map<IEnumerable<Round>, IEnumerable<RoundViewModel>>(tournaments));
        }

        [HttpGet("GetByTournamentAndGambler/{tournamentId}/{gamblerId}")]
        public async Task<IActionResult> GetByTournamentAndGambler(int tournamentId, int gamblerId)
        {
            var rounds = await _roundService.GetByTournamentAndGambler(tournamentId, gamblerId);

            return Json(rounds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tournament = await _roundService.Get(id);

            return Json(Mapper.Map<Round, RoundViewModel>(tournament));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddRoundViewModel model)
        {
            var round = await _roundService.Create(model.RoundName, model.TournamentId);

            return Json(round.Id);
        }

        [HttpPut("SaveBets")]
        public async Task<IActionResult> SaveBets([FromBody] SaveMatchBetsViewModel model)
        {
            var bets = await _matchBetService.SaveBets(model);

            return Json(Mapper.Map<IEnumerable<MatchBet>, IEnumerable<MatchBetViewModel>>(bets));
        }

    }
}