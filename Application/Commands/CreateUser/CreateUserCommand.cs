using API_Application.Application.DTOs;
using API_Application.Models;
using MediatR;

namespace API_Application.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDTO>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<Address>? Addresses { get; set; }

        public CreateUserCommand(string? firstName, string? lastName, string? email, string? contactNo, List<Address>? addresses)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNo = contactNo;
            Addresses = addresses;
        }
    }
}
