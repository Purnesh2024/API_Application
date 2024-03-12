using API_Application.Application.DTOs;
using API_Application.Infrastructure.EmployeeRepository;
using API_Application.Middleware;
using Microsoft.Extensions.Logging;
using MediatR;

namespace API_Application.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<UpdateEmployeeHandler> _logger;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, ILogger<UpdateEmployeeHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDTO> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(command.EmpUuid);
                if (employee == null)
                {
                    throw new NotFoundException("Employee Not Found");
                }

                employee.FirstName = command.FirstName;
                employee.LastName = command.LastName;
                employee.Email = command.Email;
                employee.ContactNo = command.ContactNo;

                await _employeeRepository.UpdateEmployeeAsync(employee);
                return employee;
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Employee not found.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the UpdateEmployeeCommand.");
                throw;
            }
        }
    }
}
