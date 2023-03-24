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
    public class PersonalDatumService : IPersonalDatumService
    {
        private readonly IUnitOfWorkHapus _uok;
        private readonly IUserService _user;
        private readonly IMapper _mapper;
        public PersonalDatumService(IUnitOfWorkHapus uok, IMapper mapper, IUserService user)
        {
            _uok = uok;
            _mapper = mapper;
            _user = user;
        }
        public async Task DeleteProfile(Guid id)
        {
            await _user.DesactivateUser(id);
        }

        public async Task EditProfile(PersonalDatumDTO personalDatumDTO)
        {
            PersonalDatum personalDatum = _mapper.Map<PersonalDatum>(personalDatumDTO);
            _uok.GetRepository<PersonalDatum>().Update(personalDatum);
            await _uok.SaveChangesAsync();
        }

        public async Task<PersonalDatumDTO> GetProfileById(Guid idUser)
        {
            UserDTO userDTO = await _user.GetUserById(idUser);
            PersonalDatum personal = await _uok.GetRepository<PersonalDatum>().GetByIdAsync(userDTO.IdPersonalData);
            return _mapper.Map<PersonalDatumDTO>(personal);
        }

        public async Task<IEnumerable<PersonalDatumDTO>> GetProfiles()
        {
            IEnumerable<PersonalDatum> listOfPersonalDatum = await _uok.GetRepository<PersonalDatum>().GetAllAsync();
            return _mapper.Map<IEnumerable<PersonalDatumDTO>>(listOfPersonalDatum);
        }
    }
}