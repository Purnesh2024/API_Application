using API_Application.Application.DTOs;

namespace API_Application.Application.Commands.UpdateUser
{
    public class UpdateUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<AddressDTO>? Addresses { get; set; }
    }
}
