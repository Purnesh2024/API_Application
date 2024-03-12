using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
    {
        public Guid EmpUuid { get; set; }
    }
}
