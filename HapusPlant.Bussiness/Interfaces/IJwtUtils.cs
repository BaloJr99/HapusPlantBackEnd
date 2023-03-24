using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;

namespace HapusPlant.Bussiness.Interfaces
{
    public interface IJwtUtils
    {
        string GenerateToken(UserDTO user);
        JwtSecurityToken ValidateToken(string token);
    }
}