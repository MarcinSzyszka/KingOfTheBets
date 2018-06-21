using AutoMapper;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Services;
using BetsKing.Shared.ViewModels.Auth;
using BetsKing.Shared.ViewModels.Gambler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetsKing.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamblerController : Controller
    {
        private readonly IGamblerService _gamblerService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public GamblerController(IGamblerService gamblerService, IHttpContextAccessor httpContextAccessor)
        {
            _gamblerService = gamblerService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gambler = await _gamblerService.GetGambler(int.Parse(User.FindFirst("Id").Value));

            return Json(Mapper.Map<Gambler, GamblerViewModel>(gambler));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var gambler = await _gamblerService.GetGambler(id);

            return Json(Mapper.Map<Gambler, GamblerViewModel>(gambler));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddGamblerViewModel model)
        {
            var gambler = await _gamblerService.Create(model);

            return Json(gambler.Id);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var gamblers = await _gamblerService.GetAll();

            return Json(Mapper.Map<IEnumerable<Gambler>, IEnumerable<GamblerViewModel>>(gamblers));
        }

        [HttpGet("GetNotAuthorized")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotAuthorized()
        {
            var authenticationResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (authenticationResult.Succeeded)
            {
                var gambler = await _gamblerService.GetGambler(int.Parse(authenticationResult.Principal.FindFirst("Id").Value));

                return Json(Mapper.Map<Gambler, GamblerViewModel>(gambler));
            }

            return Json(new object());
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var token = await _gamblerService.Login(model);

            return Json(token);
        }
    }
}