using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public Guid UserUuid { get; set; }
    }
}
