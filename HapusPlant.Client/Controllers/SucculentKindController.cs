using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HapusPlant.Bussiness.Interfaces;
using HapusPlant.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HapusPlant.Client.Controllers
{
    public class SucculentKindController : BaseController
    {
        private readonly ISucculentKindService _succulentKind;
        private readonly ISucculentFamilyService _succulentFamily;
        private readonly IMapper _mapper;
        public SucculentKindController(ISucculentKindService succulentKind, IMapper mapper, ISucculentFamilyService succulentFamily)
        {
            _succulentKind = succulentKind;
            _mapper = mapper;
            _succulentFamily = succulentFamily;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateSucculentKind([FromBody]SucculentKindDTO succulentKindDTO){
            if(ModelState.IsValid){
                succulentKindDTO.IdUser = userData.IdUser;
                await _succulentKind.CreateSucculentKind(succulentKindDTO);
                return Ok(new { success = true });
            }
            throw new ApplicationException("Empty Fields");
        }

        [HttpGet]
        public async Task<ActionResult> Index(){
            IEnumerable<SucculentKindDTO>  listOfSucculentsWithOutFamilies = await _succulentKind.GetSucculentKinds(userData.IdUser);
            List<SearchSucculentKindDTO> listOfSucculentsWithFamilies = new List<SearchSucculentKindDTO>();
            foreach (var succulent in listOfSucculentsWithOutFamilies)
            {
                SearchSucculentKindDTO search = _mapper.Map<SearchSucculentKindDTO>(succulent);
                search.SucculentFamily = (await _succulentFamily.GetSucculentFamilyById(succulent.IdSucculentFamily, userData.IdUser)).Family;
                listOfSucculentsWithFamilies.Add(search);
            }
            return Ok(listOfSucculentsWithFamilies);
        }

        [HttpGet]
        [Route("/SucculentKind/{idSucculentKind}")]
        public async Task<ActionResult> Index(Guid idSucculentKind){
            return Ok(await _succulentKind.GetSucculentKindById(idSucculentKind, userData.IdUser));
        }

        [HttpPut]
        public async Task<ActionResult> EditSucculentKind([FromBody]SucculentKindDTO succulentKindDTO){
            if(ModelState.IsValid){
                succulentKindDTO.IdUser = userData.IdUser;
                await _succulentKind.EditSucculentKind(succulentKindDTO);
                return Ok(new { success = true });
            }
            throw new ApplicationException("Empty Fields");
        }

        [HttpDelete]
        [Route("/SucculentKind/DeleteSucculentKind/{idSucculentKind}")]
        public async Task<ActionResult> DeleteSucculentKind(Guid idSucculentKind){
            await _succulentKind.DeleteSucculentKind(idSucculentKind);
            return Ok(new { success = true });
        }
    }
}