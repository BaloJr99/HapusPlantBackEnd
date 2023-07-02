using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;
using HapusPlant.Data.Models;

namespace HapusPlant.Bussiness.Interfaces
{
    public interface ISharedCollectionService
    {
        public Task CreateSharedCollection(SharedCollectionDTO sharedCollectionDTO);
        public Task<SharedCollection> SharedCollectionExists(SharedCollectionDTO sharedCollectionDTOr);
        public Task DeleteSharedCollection(SharedCollectionDTO sharedCollectionDTO);
        public Task<IEnumerable<Guid>> GetSharedCollections(Guid idUser);
        public Task<IEnumerable<SharedUserDTO>> GetSharedCollectionsUsers(Guid idUser);
    }
}