using BlogSite.API.Models;
using BlogSite.DataAccsess.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogSite.Business.Authentication
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        public TokenHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string CreateToken(User user, string role, int hours)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

            var identity = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, user.Email),
            });
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(hours),
                SigningCredentials = signingCredentials,
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

            //Token token = new();

            //SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //var expiration = DateTime.UtcNow.AddHours(hours);
            //JwtSecurityToken securityToken = new(
            //    audience: _configuration["Token:Audience"],
            //    issuer: _configuration["Token:Issuer"],
            //    expires: expiration,
            //    notBefore: DateTime.UtcNow,
            //    signingCredentials: signingCredentials,
            //    claims: new List<Claim> {
            //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //        new Claim(ClaimTypes.Role, role),
            //        new Claim(ClaimTypes.Email, user.Email),
            //    });

            ////Token oluşturucu sınıfından bir örnek alalım.
            //JwtSecurityTokenHandler tokenHandler = new();
            //token.AccessToken = tokenHandler.WriteToken(securityToken);
            //token.Expiration = expiration;
            //token.RefreshToken = CreateRefreshToken();
            //return token;
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            var check = _userRepository.CheckUserRefreshTokenExists(refreshToken);
            if (check)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"])),
                ValidateLifetime = false
            };
            var handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = handler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                throw new SecurityTokenException("Invalid token!");
            return principal;
        }

        //public string CreateToken(User user, string role)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

        //    var identity = new ClaimsIdentity(new Claim[] {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Role, role),
        //        new Claim(ClaimTypes.Email, user.Email),
        //    });
        //    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {

        //        Subject = identity,
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = signingCredentials,
        //    };
        //    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //    return jwtTokenHandler.WriteToken(token);
        //}

        //public string CreateRefreshToken()
        //{
        //    var tokenBytes = RandomNumberGenerator.GetBytes(64);
        //    var refreshToken = Convert.ToBase64String(tokenBytes);
        //    var check =  _userRepository.CheckUserRefreshTokenExists(refreshToken);
        //    if (check)
        //    {
        //        return CreateRefreshToken();
        //    }
        //    return refreshToken;
        //}

        //public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        //{
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false,
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"])),
        //        ValidateLifetime = false
        //    };
        //    var handler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;
        //    var principal = handler.ValidateToken(token, tokenValidationParameters, out securityToken);
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
        //        throw new SecurityTokenException("Invalid token!");
        //    return principal;
        //}

    }
}
