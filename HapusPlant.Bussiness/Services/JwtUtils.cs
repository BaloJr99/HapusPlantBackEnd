using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using HapusPlant.Bussiness.Interfaces;
using HapusPlant.Common;
using HapusPlant.Data.DTO;

namespace PersonalProject.Business.Services
{
    public class JwtUtils : IJwtUtils
    {
        private readonly Jwt _appSettings;

        public JwtUtils(IOptions<Jwt>  appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(UserDTO userLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create claims
            var claims = new []{
                new Claim("id", userLogin.IdUser.ToString()),
                new Claim(ClaimTypes.Name, userLogin.Username),
                new Claim(ClaimTypes.Role, userLogin.Role)
            };

            //Create token
            var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Audience, claims, expires: DateTime.Now.AddDays(7),signingCredentials: creadentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken ValidateToken(string token) {
            if (token == null) 
                return null!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            try{
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _appSettings.Audience,
                    ValidIssuer = _appSettings.Issuer,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // return user id from JWT token if validation successful
                return jwtToken;
            } catch {
                // return null if validation fails
                return null!;
            }
        }
    }
}