using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;

namespace HapusPlant.Bussiness.Interfaces
{
    public interface ISucculentFamilyService
    {
        public Task CreateSucculentFamily(SucculentFamilyDTO succulentFamilyDTO);
        public Task EditSucculentFamily(SucculentFamilyDTO succulentFamilyDTO);
        public Task DeleteSucculentFamily(Guid idSucculentFamily);
        public Task<IEnumerable<SucculentFamilyDTO>> GetSucculentFamilies(Guid idUser);
        public Task<SucculentFamilyDTO> GetSucculentFamilyById(Guid idSucculentFamily, Guid idUser);
    }
}