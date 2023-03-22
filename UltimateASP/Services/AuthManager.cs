
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UltimateASP.Data.EntityClasses;
using UltimateASP.Models;

namespace UltimateASP.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _apiUser;

        

        public AuthManager(UserManager<ApiUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var signingCredential = GetSigningCredentils();
            var claims = await GetClaims();
            var token = GenerateToken(signingCredential, claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signingCredential, List<Claim> claims)
        {
            var JwtSettings = _configuration.GetSection("Jwt");
            var token = new JwtSecurityToken(
                    issuer: JwtSettings.GetSection("Issuer").Value,
                    audience: JwtSettings.GetSection("Audience").Value,
                    claims: claims,
                    signingCredentials: signingCredential,
                    expires: DateTime.UtcNow.AddDays(7)
                );
            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _apiUser.UserName),
                new Claim(ClaimTypes.MobilePhone, _apiUser.PhoneNumber)
            };
            var roles = await _userManager.GetRolesAsync(_apiUser);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSigningCredentils()
        {
            var Key = Environment.GetEnvironmentVariable("SECRET");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(LoginUserDTO loginUserDTO)
        {
            _apiUser = await _userManager.FindByNameAsync(loginUserDTO.Email);
            return (_apiUser != null && await _userManager.CheckPasswordAsync(_apiUser
                , loginUserDTO.Password));
        }
    }
}
