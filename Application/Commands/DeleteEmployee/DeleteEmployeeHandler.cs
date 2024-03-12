using API_Application.Application.DTOs;
using API_Application.Infrastructure.EmployeeRepository;
using Microsoft.Extensions.Logging;
using MediatR;
using API_Application.Middleware;

namespace API_Application.Application.Commands.DeleteEmployee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, EmployeeDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<DeleteEmployeeHandler> _logger;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, ILogger<DeleteEmployeeHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDTO> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(command.EmpUuid);
                if (employee == null)
                {
                    throw new NotFoundException("Employee not found !!!");
                }

                return await _employeeRepository.DeleteEmployeeAsync(employee.EmpUuid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the DeleteEmployeeCommand.");
                throw;
            }
        }
    }
}
