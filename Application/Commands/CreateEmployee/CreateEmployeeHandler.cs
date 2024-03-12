using API_Application.Application.Commands.CreateAddress;
using API_Application.Application.DTOs;
using API_Application.Infrastructure.EmployeeRepository;
using API_Application.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API_Application.Application.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CreateEmployeeHandler> _logger;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, ILogger<CreateEmployeeHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDTO> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var employee = new EmployeeDTO()
                {
                    EmpUuid = Guid.NewGuid(),
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email,
                    ContactNo = command.ContactNo,
                    Addresses = (List<AddressDTO>?)command.Addresses
                };
                var addedEmployee = await _employeeRepository.AddEmployeeAsync(employee);

                if (addedEmployee != null)
                {
                    return addedEmployee;
                }
                else
                {
                    throw new ValidationException("Failed to add the Employee");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the CreateEmployeeCommand.");
                throw;
            }
        }
    }
}
