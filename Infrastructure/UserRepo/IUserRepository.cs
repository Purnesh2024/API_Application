using API_Application.Application.DTOs;
using API_Application.Models;

namespace API_Application.Infrastructure.UserRepo.UserRepository
{
    public interface IUserRepository
    {
        public Task<List<UserDTO>> GetUserListAsync(int Offset, int limit);
        public Task<UserDTO> GetUserByIdAsync(Guid UserUuid);
        public Task<UserDTO> AddUserAsync(User user);
        public Task<UserDTO> UpdateUserAsync(UserDTO user);
        public Task<UserDTO> DeleteUserAsync(Guid UserUuid);        
    }
}
