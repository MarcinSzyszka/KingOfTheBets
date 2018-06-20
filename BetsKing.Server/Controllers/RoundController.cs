using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Services.Rounds;
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

        public RoundController(IRoundService roundService)
        {
            _roundService = roundService;
        }

        [HttpGet("GetByTournament/{tournamentId}")]
        public async Task<IActionResult> GetByTournament(int tournamentId)
        {
            var tournaments = await _roundService.GetAll(tournamentId);

            return Json(Mapper.Map<IEnumerable<Round>, IEnumerable<RoundViewModel>>(tournaments));
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
    }
}