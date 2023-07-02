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
    public class SharedCollectionController : BaseController
    {
        private readonly ISharedCollectionService _sharedCollection;
        public SharedCollectionController(ISharedCollectionService sharedCollection)
        {
            _sharedCollection = sharedCollection;
        }

        [HttpGet]
        public async Task<ActionResult> Index(){
            return Ok(await _sharedCollection.GetSharedCollectionsUsers(userData.IdUser));
        }

        [HttpPost]
        public async Task<ActionResult> CreateSharedCollection([FromBody]SharedCollectionDTO sharedCollectionDTO){
            if(ModelState.IsValid){
                sharedCollectionDTO.IdOriginalUser = userData.IdUser;
                await _sharedCollection.CreateSharedCollection(sharedCollectionDTO);
                return Ok(new { success = true });
            }
            throw new ApplicationException("Empty Fields");
        }

        [HttpPost]
        [Route("/SharedCollection/AddSharedCollection/{idSharedUser}")]
        public async Task<ActionResult> AddSharedCollection(Guid idSharedUser){
            SharedCollectionDTO sharedCollectionDTO = new SharedCollectionDTO();
            sharedCollectionDTO.IdOriginalUser = userData.IdUser;
            sharedCollectionDTO.IdSharedUser = idSharedUser; 
            await _sharedCollection.CreateSharedCollection(sharedCollectionDTO);
            return Ok(new { success = true });
        }

        [HttpDelete]
        [Route("/SharedCollection/DeleteSharedCollection/{idSharedUser}")]
        public async Task<ActionResult> DeleteSharedCollection(Guid idSharedUser){
            SharedCollectionDTO sharedCollectionDTO = new SharedCollectionDTO();
            sharedCollectionDTO.IdOriginalUser = userData.IdUser;
            sharedCollectionDTO.IdSharedUser = idSharedUser;
            await _sharedCollection.DeleteSharedCollection(sharedCollectionDTO);
            return Ok(new { success = true });
        }
    }
}