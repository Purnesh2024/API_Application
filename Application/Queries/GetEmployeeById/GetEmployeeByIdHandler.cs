using API_Application.Application.DTOs;
using API_Application.Infrastructure.EmployeeRepository;
using API_Application.Middleware;
using Microsoft.Extensions.Logging;
using MediatR;

namespace API_Application.Application.Queries.GetEmployeeById
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeByIdHandler> _logger;

        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeByIdHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(query.EmpUuid);

                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new NotFoundException("Employee not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the GetEmployeeByIdQuery.");
                throw;
            }
        }
    }
}