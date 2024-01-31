using API_Application.Application.DTOs;
using API_Application.Infrastructure.UserRepo.UserRepository;
using API_Application.Models;
using MediatR;

namespace API_Application.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserUuid = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                ContactNo = command.ContactNo,
                Addresses = (List<Address>?)command.Addresses
            };
            return await _userRepository.AddUserAsync(user);
        }
    }
}
