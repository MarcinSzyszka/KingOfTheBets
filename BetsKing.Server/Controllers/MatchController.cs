using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Services.Matches;
using BetsKing.Shared.ViewModels.Matches;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetsKing.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MatchController : Controller
    {
        readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet("GetByRound/{roundId}")]
        public async Task<IActionResult> GetByRound(int roundId)
        {
            var matches = await _matchService.GetAll(roundId);

            return Json(Mapper.Map<IEnumerable<Match>, IEnumerable<MatchViewModel>>(matches));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tournament = await _matchService.Get(id);

            return Json(Mapper.Map<Match, MatchViewModel>(tournament));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddMatchViewModel model)
        {
            var round = await _matchService.Create(model);

            return Json(round.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateMatchScoreViewModel model)
        {
            var success = await _matchService.UpdateScore(model);

            return Json(success);
        }
    }
}