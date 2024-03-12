using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<List<EmployeeWithoutEmpUuidDTO>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Search { get; set; }
    }
}
