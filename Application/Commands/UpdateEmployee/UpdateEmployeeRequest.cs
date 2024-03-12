using API_Application.Application.DTOs;

namespace API_Application.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeRequest
    {
        public Guid EmpUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
    }
}
