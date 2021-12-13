using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RefitSamples.Configuration;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using RefitSamples.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RefitSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtConfig _jwtConfig;


        public AccountController(
                UserManager<AppUser> userManager,
                IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        public async Task<IActionResult> Register([FromBody] UserRegistrationInput user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return AuthFail("Email already in use");
                }
                var newUser = new AppUser() { Email = user.Email, UserName = user.Username };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    return StatusCode(StatusCodes.Status200OK, new AuthResult()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return AuthFail(isCreated.Errors.Select(x => x.Description));
                }
            }
            return AuthFail("Invalid payload");
        }

        private string GenerateJwtToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }


        private ObjectResult AuthFail(string msg = null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new AuthResult()
            {
                Errors = new List<string>() { msg ?? "Invalid login request" },
                Success = false
            });
        }
        private ObjectResult AuthFail(IEnumerable<string> errors)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new AuthResult()
            {
                Errors = errors.ToList(),
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResult))]
        public async Task<IActionResult> Login([FromBody] UserLoginInput user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return AuthFail();
                }
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    return AuthFail();
                }
                var jwtToken = GenerateJwtToken(existingUser);
                return StatusCode(StatusCodes.Status200OK, new AuthResult()
                {
                    Success = true,
                    Token = jwtToken
                });
            }
            return AuthFail();
        }
    }
}
