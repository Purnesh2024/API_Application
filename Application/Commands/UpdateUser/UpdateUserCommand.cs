using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public Guid UserUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<AddressDTO>? Addresses { get; set; }

        public UpdateUserCommand(string? firstName, string? lastName, string? email, string? contactNo, List<AddressDTO>? addresses)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNo = contactNo;
            Addresses = addresses;
        }
    }
}
