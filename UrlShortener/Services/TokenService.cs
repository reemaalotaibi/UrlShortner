﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UrlShortener.API.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            //var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Issuer = _configuration["JWTKey:ValidIssuer"],
            //    Audience = _configuration["JWTKey:ValidAudience"],
            //    //Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
            //    Expires = DateTime.UtcNow.AddMinutes(1),
            //    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            //    Subject = new ClaimsIdentity(claims)
            //};

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);

            return "Token";
        }
    }
}
