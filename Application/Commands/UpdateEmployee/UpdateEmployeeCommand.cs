using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDTO>
    {
        public Guid EmpUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }

        public UpdateEmployeeCommand(Guid empUuid, string? firstName, string? lastName, string? email, string? contactNo)
        {
            EmpUuid = empUuid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNo = contactNo;
        }
    }
}
