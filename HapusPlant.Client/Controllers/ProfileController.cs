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
    public class ProfileController : Controller
    {
        private readonly IPersonalDatumService _personalDatum;
        public ProfileController(IPersonalDatumService personalDatum)
        {
            _personalDatum = personalDatum;
        }

        [HttpGet]
        public async Task<ActionResult> Index(){
            var profiles = await _personalDatum.GetProfiles();
            return Ok(new { profiles });
        }

        [HttpGet]
        [Route("/Profile/{idUser}")]
        public async Task<ActionResult> Index(Guid idUser){
            var profile = await _personalDatum.GetProfileById(idUser);
            return Ok(profile);
        }

        [HttpPut]
        public async Task<ActionResult> EditProfile([FromBody]PersonalDatumDTO personalDatumDTO){
            if(ModelState.IsValid){
                await _personalDatum.EditProfile(personalDatumDTO);
                return Ok(new { success = true });
            }
            return Ok(new { success = false });
        }

        [HttpDelete]
        [Route("/Profile/DeleteProfile/{idUser}")]
        public async Task<ActionResult> DeleteProfile(Guid idUser){
            await _personalDatum.DeleteProfile(idUser);
            return Ok(new { success = true });
        }
    }
}