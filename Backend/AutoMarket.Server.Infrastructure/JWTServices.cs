using AutoMarket.Server.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AutoMarket.Server.Infrastructure
{
    public class JWTServices
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SymmetricSecurityKey _jwtKey;
        public JWTServices(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;

            // jwt ключ викориситаний для шифрування та розшифрування jwt токенів
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }
        public async Task<string> CreateJWT(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.GivenName, user.LastName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwt);
        }

        public RefreshToken CreateRefreshToken(User user)
        {
            var token = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(token);

            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(token),
                User = user,
                DateExpiresUtc = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"]))
            };

            return refreshToken;
        }
    }
}
