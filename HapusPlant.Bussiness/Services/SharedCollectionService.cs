using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HapusPlant.Bussiness.Interfaces;
using HapusPlant.Bussiness.UnitOfWork.Interfaces;
using HapusPlant.Data.DTO;
using HapusPlant.Data.Models;

namespace HapusPlant.Bussiness.Services
{
    public class SharedCollectionService : ISharedCollectionService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHapus _uok;
        public SharedCollectionService(IUnitOfWorkHapus uok, IMapper mapper)
        {
            _uok = uok;
            _mapper = mapper;
        }
        
        public async Task CreateSharedCollection(SharedCollectionDTO sharedCollectionDTO)
        {
            var sharedCollection = SharedCollectionExists(sharedCollectionDTO);
            if(sharedCollection != null){
                SharedCollection sharedCollectionToUpdate = _mapper.Map<SharedCollection>(sharedCollection);
                sharedCollectionToUpdate.IsShared = true;
                _uok.GetRepository<SharedCollection>().Update(sharedCollectionToUpdate);
                await _uok.SaveChangesAsync();
            }else{
                await _uok.GetRepository<SharedCollection>().AddAsync(_mapper.Map<SharedCollection>(new SharedCollectionDTO()));
                await _uok.SaveChangesAsync();
            }
        }

        public async Task DeleteSharedCollection(SharedCollectionDTO sharedCollectionDTO)
        {
            var sharedCollection = await SharedCollectionExists(sharedCollectionDTO);
            if(sharedCollection != null){
                sharedCollection.IsShared = false;
                _uok.GetRepository<SharedCollection>().Update(sharedCollection);
                await _uok.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Guid>> GetSharedCollections(Guid idUser)
        {
            return (await _uok.GetRepository<SharedCollection>().GetWhereAsync(x => x.IdSharedUser == idUser && x.IsShared)).Select(x => x.IdOriginalUser);
        }

        public async Task<SharedCollection> SharedCollectionExists(SharedCollectionDTO sharedCollectionDTO)
        {
            return ((await _uok.GetRepository<SharedCollection>().GetWhereAsync(x => x.IdOriginalUser == sharedCollectionDTO.IdOriginalUser && x.IdSharedUser == sharedCollectionDTO.IdSharedUser))).FirstOrDefault();
        }

        public async Task<IEnumerable<SharedUserDTO>> GetSharedCollectionsUsers(Guid idUser){
            IEnumerable<Guid> idSharedUsers = (await _uok.GetRepository<SharedCollection>().GetAsync(x => x.IdOriginalUser == idUser && x.IsShared, includeProperties: "IdSharedUserNavigation")).Select(x => x.IdSharedUser);
            IEnumerable<SharedUserDTO> shared = Enumerable.Empty<SharedUserDTO>();
            if(idSharedUsers.Count() > 0){
                var sharedUsers = await _uok.GetRepository<User>().GetAsync(x => idSharedUsers.Contains(x.IdUser), includeProperties: "IdPersonalDataNavigation");
                shared = _mapper.Map<IEnumerable<SharedUserDTO>>(sharedUsers);
            }
            return shared.OrderBy(x => x.FullName);
        }
    }
}