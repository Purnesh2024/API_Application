using API_Application.Application.DTOs;
using API_Application.Models;

namespace API_Application.Application.Commands.CreateEmployee
{
    public class CreateEmployeeRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<AddressDTO>? Addresses { get; set; }
    }
}
