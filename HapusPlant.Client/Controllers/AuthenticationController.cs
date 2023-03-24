using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Bussiness.Interfaces;
using HapusPlant.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HapusPlant.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _user;
        private readonly IJwtUtils _jwtUtils;
        public AuthenticationController(IUserService user, IJwtUtils jwtUtils)
        {
            _user = user;
            _jwtUtils = jwtUtils;
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser([FromBody]UserDTO userDTO)
        {
            if(ModelState.IsValid){
                UserDTO? user = await _user.CheckCredentials(userDTO);
                if(user != null){
                    var token = _jwtUtils.GenerateToken(user);
                    if(token != null){
                        HttpContext.Session.SetString("Token", token);
                        HttpContext.Response.Cookies.Append("X-Access-Token", token, new CookieOptions(){HttpOnly = true, Expires = DateTime.Now.AddDays(7),  });
                    }else{
                        return Ok(new { success  = false });
                    }
                }else{
                    return Ok(new { success  = false });
                }
                return Ok(new { success  = true });
            }
            throw new Exception("Incorrect Username/Password");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}