using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HapusPlant.Data.DTO;

namespace HapusPlant.Bussiness.Interfaces
{
    public interface IUserService
    {
        public Task<Guid> CreateUser(NewUserDTO newUser);
        public Task<bool> EditUser();
        public Task DesactivateUser(Guid idPersonalData);
        public Task<bool> CheckDuplicate(UserDTO userDTO);
        Task<UserDTO?> CheckCredentials(UserDTO userDTO);
        Task<UserDTO> GetUserById(Guid guid);
        Task<IEnumerable<UserDTO>> GetUsers();
    }
}