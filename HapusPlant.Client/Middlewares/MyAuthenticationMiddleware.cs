using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HapusPlant.Bussiness.Interfaces;

namespace HapusPlant.Client.Middleware
{
    public class MyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public MyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils _jwtUtil, IUserService _user){

            var token = context.Session.GetString("Token");
            if(token == null)
                if(context.Request.Cookies.ContainsKey("X-Access-Token")){
                    token = context.Request.Cookies["X-Access-Token"];
                }

            JwtSecurityToken validatedToken = null!;
            if(token != null && token != ""){
                validatedToken = _jwtUtil.ValidateToken(token);
            }

            if(validatedToken == null){
                context.Session.Clear();
            }else{
                context.Request.Headers.Add("Authorization", "Bearer " + token);
                context.Items["UserData"] = await _user.GetUserById(Guid.Parse(validatedToken.Payload["id"].ToString()));

            }
            await _next(context);
        }
    }
}