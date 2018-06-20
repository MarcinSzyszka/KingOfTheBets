using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BetsKing.Server.Data.Context;
using BetsKing.Server.Data.Entity;
using BetsKing.Server.Models;
using BetsKing.Shared.ViewModels.Auth;
using BetsKing.Shared.ViewModels.Gambler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BetsKing.Server.Services
{
    public class GamblerService : IGamblerService
    {
        private readonly BetsKingDbContext _dbContext;
        private readonly PasswordHasher<Gambler> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthSettings _authSettings;

        public GamblerService(BetsKingDbContext dbContext, IHttpContextAccessor httpContextAccessor, IOptions<AuthSettings> authSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher<Gambler>();
            _httpContextAccessor = httpContextAccessor;
            _authSettings = authSettings.Value;
        }

        public async Task<Gambler> Create(AddGamblerViewModel model)
        {
            var gambler = await _dbContext.Gamblers.FirstOrDefaultAsync(g => g.UserName.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase) &&
                                                                        g.DisplayName.Equals(model.DisplayName, StringComparison.InvariantCultureIgnoreCase));

            if (gambler == null)
            {
                var passwordHasher = new PasswordHasher<Gambler>();

                gambler = new Gambler
                {
                    UserName = model.Name,
                    DisplayName = model.DisplayName
                };

                gambler.Password = passwordHasher.HashPassword(gambler, model.Password);

                await _dbContext.Gamblers.AddAsync(gambler);
                await _dbContext.SaveChangesAsync();
            }

            return gambler;
        }

        public async Task<IEnumerable<Gambler>> GetAll()
        {
            return await _dbContext.Gamblers.Include(g => g.Roles).ThenInclude(r => r.Role).ToListAsync();
        }

        public Task<Gambler> GetGambler(int id)
        {
            return _dbContext.Gamblers.Include(g => g.Roles).ThenInclude(r => r.Role).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<string> Login(LoginViewModel model)
        {
            var gambler = await _dbContext.Gamblers.Include(g => g.Roles).ThenInclude(r => r.Role).FirstOrDefaultAsync(g => g.UserName.Equals(model.UserName, StringComparison.InvariantCultureIgnoreCase));

            if (gambler == null)
            {
                return null;
            }

            if (_passwordHasher.VerifyHashedPassword(gambler, gambler.Password, model.Password) != PasswordVerificationResult.Failed)
            {
                var roles = gambler.Roles.Select(r => r.Role);

                var rolesNames = string.Join(',', roles.Select(r => r.Name));

                var claims = new ClaimsPrincipal();

                claims.AddIdentity(new ClaimsIdentity(new List<Claim>
                {
                    new Claim( "Id", gambler.Id.ToString()),
                    new Claim( "DisplayName", gambler.DisplayName),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, gambler.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, rolesNames)
                }, JwtBearerDefaults.AuthenticationScheme));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_authSettings.Issuer,
                    _authSettings.Audience,
                    claims: claims.Claims,
                    expires: DateTime.Now.AddMinutes(_authSettings.TokenLifeTimeInMinutes),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return null;
            }
        }
    }
}
