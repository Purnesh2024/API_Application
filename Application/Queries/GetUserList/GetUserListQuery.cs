using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<List<UserDTO>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
