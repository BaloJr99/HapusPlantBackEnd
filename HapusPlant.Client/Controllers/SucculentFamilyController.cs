using HapusPlant.Bussiness.Interfaces;
using HapusPlant.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HapusPlant.Client.Controllers
{
    public class SucculentFamilyController : BaseController
    {
        private readonly ISucculentFamilyService _succulentFamily;
        public SucculentFamilyController(ISucculentFamilyService succulentFamily)
        {
            _succulentFamily = succulentFamily;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateSucculentFamily([FromBody]SucculentFamilyDTO succulentFamilyDTO){
            if(ModelState.IsValid){
                succulentFamilyDTO.IdUser = userData.IdUser;
                await _succulentFamily.CreateSucculentFamily(succulentFamilyDTO);
                return Ok(new { success = true });
            }
            throw new ApplicationException("Empty Fields");
        }

        [HttpGet]
        public async Task<ActionResult> Index(){
            return Ok(await _succulentFamily.GetSucculentFamilies(userData.IdUser));
        }

        [HttpGet]
        [Route("/SucculentFamily/{idSucculentFamily}")]
        public async Task<ActionResult> Index(Guid idSucculentFamily){
            return Ok(await _succulentFamily.GetSucculentFamilyById(idSucculentFamily, userData.IdUser));
        }

        [HttpPut]
        public async Task<ActionResult> EditSucculentFamily([FromBody]SucculentFamilyDTO succulentFamilyDTO){
            if(ModelState.IsValid){
                succulentFamilyDTO.IdUser = userData.IdUser;
                await _succulentFamily.EditSucculentFamily(succulentFamilyDTO);
                return Ok(new { success = true });
            }
            throw new ApplicationException("Empty Fields");
        }

        [HttpDelete]
        [Route("/SucculentFamily/DeleteSucculentFamily/{idSucculentFamily}")]
        public async Task<ActionResult> DeleteSucculentFamily(Guid idSucculentFamily){
            await _succulentFamily.DeleteSucculentFamily(idSucculentFamily);
            return Ok(new { success = true });
        }
    }
}