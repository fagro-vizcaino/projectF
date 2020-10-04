using ProjectF.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using LanguageExt.Common;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using ProjectF.Data.Entities.Auth;

namespace ProjectF.Api.Features.Auth
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly AuthUserCrudHandler _operations;
        readonly IConfiguration _config;

        public AuthController(AuthUserCrudHandler operations, IConfiguration config)
        {
            _operations = operations;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterViewModel user)
         => _operations.Register(user.ToDto())
                    .Match<ActionResult>(
                        Left: err => BadRequest(err.Message),
                        Right: _ => Ok());


        [HttpPost("login")]
        public IActionResult Login(UserLoginViewModel user)
         => _operations.Login(user.ToDto())
            .Match<IActionResult>(Left: err => Unauthorized(err.Message),
              Right: e =>
              {
                  return GenerateClaims(e)
                  .Match<IActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: token => Ok(new { access_token = token })
                  );
              }
          );


        Either<Error, string> GenerateClaims(UserDto user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
      };

            SymmetricSecurityKey key = null;
            try
            {
                key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            }
            catch (System.Exception ex)
            {
                Error.New(ex.Message);
            }

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            tokenDescriptor.Expires = DateTime.Now.AddDays(1);
            tokenDescriptor.SigningCredentials = credential;

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}