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
    public class SucculentKindService : ISucculentKindService
    {
        private readonly IUnitOfWorkHapus _uok;
        private readonly ISharedCollectionService _sharedCollection;
        private readonly IMapper _mapper;
        public SucculentKindService(IUnitOfWorkHapus uok, IMapper mapper, ISharedCollectionService sharedCollection)
        {
            _uok = uok;
            _mapper = mapper;
            _sharedCollection = sharedCollection;
        }
        public async Task CreateSucculentKind(SucculentKindDTO succulentFamilyDTO)
        {
            SucculentKind succulentFamily = _mapper.Map<SucculentKind>(succulentFamilyDTO);
            await _uok.GetRepository<SucculentKind>().AddAsync(succulentFamily);
            await _uok.SaveChangesAsync();
        }

        public async Task DeleteSucculentKind(Guid idSucculentKind)
        {
            SucculentKind succulentFamily = await _uok.GetRepository<SucculentKind>().GetByIdAsync(idSucculentKind);
            _uok.GetRepository<SucculentKind>().Remove(succulentFamily);
            await _uok.SaveChangesAsync();
        }

        public async Task EditSucculentKind(SucculentKindDTO succulentFamilyDTO)
        {
            SucculentKind succulentFamily = _mapper.Map<SucculentKind>(succulentFamilyDTO);
            _uok.GetRepository<SucculentKind>().Update(succulentFamily);
            await _uok.SaveChangesAsync();
        }

        public async Task<IEnumerable<SucculentKindDTO>> GetSucculentKinds(Guid idUser)
        {
            IEnumerable<Guid> sharedCollection = await _sharedCollection.GetSharedCollections(idUser);
            return _mapper.Map<IEnumerable<SucculentKindDTO>>(await _uok.GetRepository<SucculentKind>().GetWhereAsync(x => x.IdUser == idUser));
        }

        public async Task<SucculentKindDTO> GetSucculentKindById(Guid idSucculentKind, Guid idUser)
        {
            IEnumerable<Guid> sharedCollection = await _sharedCollection.GetSharedCollections(idUser);
            SucculentKind succulenKind = await _uok.GetRepository<SucculentKind>().GetByIdAsync(idSucculentKind);
            if(succulenKind != null)
                if(succulenKind.IdUser != idUser && !sharedCollection.Contains(idUser))
                    throw new Exception("You are not authorized to implement this operation");
            return _mapper.Map<SucculentKindDTO>(succulenKind);
        }
    }
}