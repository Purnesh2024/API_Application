using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<UserDTO>
    {
        public Guid UserUuid { get; set; }
    }
}
