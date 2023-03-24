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
    public class SucculentFamilyService : ISucculentFamilyService
    {
        private readonly IUnitOfWorkHapus _uok;
        private readonly IMapper _mapper;
        public SucculentFamilyService(IUnitOfWorkHapus uok, IMapper mapper)
        {
            _uok = uok;
            _mapper = mapper;
        }
        public async Task CreateSucculentFamily(SucculentFamilyDTO succulentFamilyDTO)
        {
            SucculentFamily succulentFamily = _mapper.Map<SucculentFamily>(succulentFamilyDTO);
            await _uok.GetRepository<SucculentFamily>().AddAsync(succulentFamily);
            await _uok.SaveChangesAsync();
        }

        public async Task DeleteSucculentFamily(Guid idSucculentFamily)
        {
            SucculentFamily succulentFamily = await _uok.GetRepository<SucculentFamily>().GetByIdAsync(idSucculentFamily);
            _uok.GetRepository<SucculentFamily>().Remove(succulentFamily);
            await _uok.SaveChangesAsync();
        }

        public async Task EditSucculentFamily(SucculentFamilyDTO succulentFamilyDTO)
        {
            SucculentFamily succulentFamily = _mapper.Map<SucculentFamily>(succulentFamilyDTO);
            _uok.GetRepository<SucculentFamily>().Update(succulentFamily);
            await _uok.SaveChangesAsync();
        }

        public async Task<IEnumerable<SucculentFamilyDTO>> GetSucculentFamilies(Guid idUser)
        {
            return _mapper.Map<IEnumerable<SucculentFamilyDTO>>(await _uok.GetRepository<SucculentFamily>().GetWhereAsync(x => x.IdUser == idUser));
        }

        public async Task<SucculentFamilyDTO> GetSucculentFamilyById(Guid idSucculentFamily, Guid idUser)
        {
            SucculentFamily succulentFamily = await _uok.GetRepository<SucculentFamily>().GetByIdAsync(idSucculentFamily);
            if(succulentFamily != null)
                if(succulentFamily.IdUser != idUser)
                    throw new Exception("You are not authorized to implement this operation");
            return _mapper.Map<SucculentFamilyDTO>(succulentFamily);
        }
    }
}