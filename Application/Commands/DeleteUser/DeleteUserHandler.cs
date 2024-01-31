using API_Application.Application.DTOs;
using API_Application.Infrastructure.UserRepo.UserRepository;
using MediatR;

namespace API_Application.Application.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserUuid);
            if (user == null)
                return default;
            return await _userRepository.DeleteUserAsync(user.UserUuid);
        }
    }
}
