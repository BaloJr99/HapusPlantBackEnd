using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;

namespace HapusPlant.Bussiness.Interfaces
{
    public interface ISucculentKindService
    {
        public Task CreateSucculentKind(SucculentKindDTO succulentKindDTO);
        public Task EditSucculentKind(SucculentKindDTO succulentKindDTO);
        public Task DeleteSucculentKind(Guid idSucculentKind);
        public Task<IEnumerable<SucculentKindDTO>> GetSucculentFamilies(Guid idUser);
        public Task<SucculentKindDTO> GetSucculentKindById(Guid idSucculentKind, Guid idUser);
    }
}