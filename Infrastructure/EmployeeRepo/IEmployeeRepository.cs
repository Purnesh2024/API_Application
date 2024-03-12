using API_Application.Application.DTOs;
using API_Application.Models;

namespace API_Application.Infrastructure.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeeWithoutEmpUuidDTO>> GetEmployeeListAsync(int Offset, int limit, string search);
        public Task<EmployeeDTO> GetEmployeeByIdAsync(Guid EmpUuid);
        public Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO Employee);
        public Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO Employee);
        public Task<EmployeeDTO> DeleteEmployeeAsync(Guid EmpUuid);
    }
}
