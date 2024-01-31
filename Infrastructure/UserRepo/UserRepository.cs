using API_Application.Application.DTOs;
using API_Application.Infrastructure.Context;
using API_Application.Infrastructure.UserRepo.UserRepository;
using API_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Application.Infrastructure.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly APIContext _context;
        public UserRepository(APIContext context)
        {
            _context = context;
        }
        public async Task<UserDTO> AddUserAsync(User user)
        {
            var newUser = new User
            {
                UserUuid = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ContactNo = user.ContactNo,
                Addresses = user.Addresses
            };
            var result = _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var addedUserDto = new UserDTO
            {
                UserUuid = result.Entity.UserUuid,
                FirstName = result.Entity.FirstName,
                LastName = result.Entity.LastName,
                Email = result.Entity.Email,
                ContactNo = result.Entity.ContactNo,
                Addresses = result.Entity.Addresses?.Select(a => new AddressDTO
                {
                    AddressUuid = Guid.NewGuid(),
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    Landmark = a.Landmark,
                    City = a.City,
                    State = a.State,
                    Country = a.Country
                }).ToList()
            };

            return addedUserDto;
        }

        public async Task<UserDTO> DeleteUserAsync(Guid userUuid)
        {
            var userToDelete = await _context.Users.Include(u => u.Addresses).FirstOrDefaultAsync(x => x.UserUuid == userUuid);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid userUuid)
        {
            var user = await _context.Users.Include(u => u.Addresses).Where(x => x.UserUuid == userUuid).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var userDto = new UserDTO
            {
/*                UserUuid = user.UserUuid,
*/                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ContactNo = user.ContactNo,
                Addresses = user.Addresses?.Select(a => new AddressDTO
                {
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    Landmark = a.Landmark,
                    City = a.City,
                    State = a.State,
                    Country = a.Country
                }).ToList()
            };

            return userDto;
        }

        public async Task<List<UserDTO>> GetUserListAsync(int limit, int offset)
        {
            var users = await _context.Users.Include(u => u.Addresses).Skip(offset).Take(limit).ToListAsync();

            var userList = users.Select(user => new UserDTO
            {
                UserUuid = user.UserUuid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ContactNo = user.ContactNo,
                Addresses = user.Addresses?.Select(a => new AddressDTO
                {
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    Landmark = a.Landmark,
                    City = a.City,
                    State = a.State,
                    Country = a.Country
                }).ToList()
            }).ToList();

            return userList;
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserUuid);

            if (existingUser == null)
            {
                throw new Exception("USER not found");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.ContactNo = user.ContactNo;

            await _context.SaveChangesAsync();

            var updatedUserDto = new UserDTO
            {
                UserUuid = existingUser.UserUuid,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                ContactNo = existingUser.ContactNo,
                Addresses = existingUser.Addresses?.Select(a => new AddressDTO
                {
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    Landmark = a.Landmark,
                    City = a.City,
                    State = a.State,
                    Country = a.Country
                }).ToList(),
            };

            return updatedUserDto;
        }
    }
}
