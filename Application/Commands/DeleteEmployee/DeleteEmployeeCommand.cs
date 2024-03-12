using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<EmployeeDTO>
    {
        public Guid EmpUuid { get; set; }
    }
}
