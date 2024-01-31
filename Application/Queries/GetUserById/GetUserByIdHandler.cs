using API_Application.Application.DTOs;
using API_Application.Infrastructure.UserRepo.UserRepository;
using MediatR;

namespace API_Application.Application.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByIdAsync(query.UserUuid);
        }
    }
}
