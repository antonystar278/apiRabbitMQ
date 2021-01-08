using Core.CustomException;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Core.Services;
using Core.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfigModel _jwtconfig;

        public AccountService(UserManager<ApplicationUser> userManager, IOptions<JwtConfigModel> jwtconfig)
        {
            _userManager = userManager;
            _jwtconfig = jwtconfig.Value;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user is null)
            {
                return await CreateUserWithToken(model);
            }
            var resultPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if(!resultPassword)
            {
                throw new BadRequest($"Wrong password for userName = {model.Username}");
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());
            if(user is null)
            {
                throw new NotFoundException($"User with id = {id} not found");
            }
            return user;
        }

        private async Task<AuthenticateResponse> CreateUserWithToken(AuthenticateRequest model)
        {
            var newUser = new ApplicationUser
            {
                CreationDate = DateTime.Now,
                UserName = model.Username,
            };
            var creationResult = await _userManager.CreateAsync(newUser, model.Password);
            if(!creationResult.Succeeded)
            {
                throw new BadRequest("The Password must be at least 6 characters long, contain upper case character and have specific symbol.");
            }

            var jwtToken = GenerateJwtToken(newUser);

            return new AuthenticateResponse(newUser, jwtToken);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtconfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
