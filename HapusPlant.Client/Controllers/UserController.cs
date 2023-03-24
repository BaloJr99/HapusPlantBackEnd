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
    public class UserController : Controller
    {
        private readonly IUserService _user;
        private readonly IPersonalDatumService _personalDatum;

        public UserController(IUserService user, IPersonalDatumService personalDatum)
        {
            _user = user;
            _personalDatum = personalDatum;
        }

        [HttpGet]
        public async Task<ActionResult> Index(){
            return Ok(await _user.GetUsers());
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewUser([FromBody]NewUserDTO newUserDTO)
        {
            if(ModelState.IsValid){
                Guid IdUser = await _user.CreateUser(newUserDTO);
                return Ok(new { success = true });
            }
                return Ok(new { success = false });
        }
    }
}