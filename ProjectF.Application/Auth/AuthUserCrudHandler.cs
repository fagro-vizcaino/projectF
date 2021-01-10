using ProjectF.Data.Repositories;
using System;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Application.Auth
{
    public class AuthUserCrudHandler
    {
        readonly UserRepository _userRepository;
        readonly CountryRepository _countryRepository;
        readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthUserCrudHandler(UserRepository userRepository
            , CountryRepository countryRepository
            , UserManager<User> userManager
            , IConfiguration configuration)
        {
            _userRepository    = userRepository;
            _countryRepository = countryRepository;
            _userManager       = userManager;
            _configuration     = configuration;
        }

        public Either<Error, RegisterUserDto> Register(RegisterUserDto user)
           => ValidateCountry(user)
               .Bind(SetCountry);

        public async Task<bool> ValidateUser(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByNameAsync(userLoginDto.Email);
            return (_user is not null 
                && await _userManager.CheckPasswordAsync(_user, userLoginDto.Password)
                && await _userManager.IsEmailConfirmedAsync(_user));
        }


        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET") ?? string.Empty);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, _user.UserName)
                , new (ClaimTypes.NameIdentifier, _user.Id)
                , new ("companyId", _user?.CompanyId.ToString() ?? "")
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expiresInMinutes").Value)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
      
        Either<Error, RegisterUserDto> ValidateCountry(RegisterUserDto user)
            => (user.SelectedCountry > 0 || user.Country != null) switch {
                true => user,
                _ => Error.New("country is required")
            };
        

        Either<Error, RegisterUserDto> SetCountry(RegisterUserDto user)
            => _countryRepository.FromCountryId(user.SelectedCountry) switch
            {
                Country c => user with { Country = c},
                _ => Error.New("couldn't find to country")
            };

        public async Task<bool> UpdateUserCompany(long companyId, string userId)
        {
            var authUser = await _userManager.FindByIdAsync(userId);
            authUser.EditUser(authUser.Firstname, authUser.Lastname, companyId);

            var result = await _userManager.UpdateAsync(authUser);

            return result.Succeeded;
        }
    }
}