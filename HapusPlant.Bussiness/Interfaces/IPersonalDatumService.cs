using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;

namespace HapusPlant.Bussiness.Interfaces
{
    public interface IPersonalDatumService
    {
        public Task EditProfile(PersonalDatumDTO personalDatumDTO);
        public Task DeleteProfile(Guid id);
        public Task<IEnumerable<PersonalDatumDTO>> GetProfiles();
        public Task<PersonalDatumDTO> GetProfileById(Guid idPersonalData);
        public Task<IEnumerable<SharedUserDTO>> SearchMatchingNames(string username, Guid idUser);
    }
}