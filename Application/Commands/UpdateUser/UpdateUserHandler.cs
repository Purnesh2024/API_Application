using API_Application.Application.DTOs;
using API_Application.Infrastructure.UserRepo.UserRepository;
using MediatR;

namespace API_Application.Application.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserUuid);
            if (user == null)
            {
                throw new Exception("USER Not Found");
            }

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;
            user.ContactNo = command.ContactNo;
            user.Addresses = (List<AddressDTO>?)command.Addresses;

            await _userRepository.UpdateUserAsync(user);
            return user;
        }
    }
}
