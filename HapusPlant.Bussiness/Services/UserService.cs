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
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkHapus _uok;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWorkHapus uok, IMapper mapper)
        {
            _uok = uok;
            _mapper = mapper;
        }

        public async Task<UserDTO?> CheckCredentials(UserDTO userDTO)
        {
            User? user = _mapper.Map<User>(userDTO);
            user = (await _uok.GetRepository<User>().GetWhereAsync(x => x.Username == userDTO.Username && x.Password == userDTO.Password)).FirstOrDefault();
            if(user != null){
                if(!user.IsActive)
                    throw new Exception("This user is inactive");
            }
            return _mapper.Map<UserDTO>(user); 
        }

        public async Task<bool> CheckDuplicate(UserDTO userDTO)
        {
            var duplicatedValue = await _uok.GetRepository<User>().GetWhereAsync(u => u.Username == userDTO.Username);
            return duplicatedValue.Count() > 0;
        }

        public async Task<Guid> CreateUser(NewUserDTO newUser)
        {
            UserDTO userDTO = _mapper.Map<UserDTO>(newUser);
            PersonalDatum personalDatum = _mapper.Map<PersonalDatum>(newUser);
            User user = _mapper.Map<User>(userDTO);
            if(!(await CheckDuplicate(userDTO))){
                try
                {
                    await _uok.CreateTransactionAsync();
                    await _uok.GetRepository<PersonalDatum>().AddAsync(personalDatum);
                    await _uok.SaveChangesAsync();
                    user.IdPersonalData = personalDatum.IdPersonalData;
                    await _uok.GetRepository<User>().AddAsync(user);
                    await _uok.SaveChangesAsync();
                    await _uok.CommitAsync();
                }
                catch (System.Exception)
                {
                    await _uok.RollbackAsync();
                    throw new Exception("An error occurred while Signing Up");
                }
                return user.IdPersonalData;
            }
            throw new Exception("This user already exists");
        }

        public async Task DesactivateUser(Guid idUser)
        {
            User user = await _uok.GetRepository<User>().GetByIdAsync(idUser);
            user.IsActive = false;
            await _uok.SaveChangesAsync();
        }

        public Task<bool> EditUser()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            return _mapper.Map<UserDTO>(await _uok.GetRepository<User>().GetByIdAsync(id));
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await _uok.GetRepository<User>().GetAllAsync());
        }
    }
}