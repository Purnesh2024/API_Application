using API_Application.Application.DTOs;
using API_Application.Infrastructure.UserRepo.UserRepository;
using MediatR;

namespace API_Application.Application.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            // Fetch users based on Limit and Offset
            var users = await _userRepository.GetUserListAsync(request.Limit, request.Offset);
            return users.Select(user => MapToUserDTO(user)).ToList();
        }

        public UserDTO MapToUserDTO(UserDTO user)
        {
            return new UserDTO
            {
                UserUuid = user.UserUuid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ContactNo = user.ContactNo,
                Addresses = user.Addresses
            };
        }
    }
}
