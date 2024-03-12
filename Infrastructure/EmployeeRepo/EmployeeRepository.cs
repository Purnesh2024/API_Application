using API_Application.Application.DTOs;
using API_Application.Infrastructure.Context;
using API_Application.Middleware;
using API_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Application.Infrastructure.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly APIContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(APIContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employee)
        {
            try
            {
                var newEmployee = new Employee
                {
                    EmpUuid = Guid.NewGuid(),
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    ContactNo = employee.ContactNo,
                    Addresses = employee.Addresses?.Select(a => new Address
                    {
                        AddressUuid = Guid.NewGuid(),
                        AddressLine1 = a.AddressLine1,
                        AddressLine2 = a.AddressLine2,
                        Landmark = a.Landmark,
                        City = a.City,
                        State = a.State,
                        Country = a.Country
                    }).ToList()
                };

                var result = _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                var addedEmployeeDto = new EmployeeDTO
                {
                    EmpUuid = result.Entity.EmpUuid,
                    FirstName = result.Entity.FirstName,
                    LastName = result.Entity.LastName,
                    Email = result.Entity.Email,
                    ContactNo = result.Entity.ContactNo,
                    Addresses = result.Entity.Addresses?.Select(a => new AddressDTO
                    {
                        AddressUuid = a.AddressUuid,
                        AddressLine1 = a.AddressLine1,
                        AddressLine2 = a.AddressLine2,
                        Landmark = a.Landmark,
                        City = a.City,
                        State = a.State,
                        Country = a.Country,
                    }).ToList()
                };

                return addedEmployeeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding employee.");
                throw;
            }
        }

        public async Task<EmployeeDTO> DeleteEmployeeAsync(Guid empUuid)
        {
            try
            {
                var EmployeeToDelete = await _context.Employees.Include(u => u.Addresses).FirstOrDefaultAsync(x => x.EmpUuid == empUuid);

                if (EmployeeToDelete != null)
                {
                    _context.Employees.Remove(EmployeeToDelete);
                    await _context.SaveChangesAsync();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee.");
                throw;
            }
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(Guid empUuid)
        {
            try
            {
                var Employee = await _context.Employees.Include(u => u.Addresses).Where(x => x.EmpUuid == empUuid).FirstOrDefaultAsync();

                if (Employee == null)
                {
                    return null;
                }

                var EmployeeDto = new EmployeeDTO
                {
                    EmpUuid = Employee.EmpUuid,
                    FirstName = Employee.FirstName,
                    LastName = Employee.LastName,
                    Email = Employee.Email,
                    ContactNo = Employee.ContactNo,
                    Addresses = Employee.Addresses?.Select(a => new AddressDTO
                    {
                        AddressUuid = a.AddressUuid,
                        AddressLine1 = a.AddressLine1,
                        AddressLine2 = a.AddressLine2,
                        Landmark = a.Landmark,
                        City = a.City,
                        State = a.State,
                        Country = a.Country
                    }).ToList()
                };

                return EmployeeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the employee.");
                throw;
            }
        }

        public async Task<List<EmployeeWithoutEmpUuidDTO>> GetEmployeeListAsync(int limit, int offset, string search)
        {
            try
            {
                var query = _context.Employees.Include(u => u.Addresses)
                .Skip(offset)
                .Take(limit);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(u =>
                        u.FirstName.Contains(search) ||
                        u.LastName.Contains(search) ||
                        u.Email.Contains(search) ||
                        u.ContactNo.Contains(search));
                }

                var Employees = await query.ToListAsync();

                var EmployeeList = Employees.Select(Employee => new EmployeeWithoutEmpUuidDTO
                {
                    EmpUuid = Employee.EmpUuid,
                    FirstName = Employee.FirstName,
                    LastName = Employee.LastName,
                    Email = Employee.Email,
                    ContactNo = Employee.ContactNo,
                    Addresses = Employee.Addresses?.Select(a => new AddressWithoutEmpUuidDTO
                    {
                        AddressUuid = a.AddressUuid,
                        AddressLine1 = a.AddressLine1,
                        AddressLine2 = a.AddressLine2,
                        Landmark = a.Landmark,
                        City = a.City,
                        State = a.State,
                        Country = a.Country
                    }).ToList()
                }).ToList();

                return EmployeeList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the employees.");
                throw;
            }
        }

        public async Task<EmployeeDTO> UpdateEmployeeAsync(EmployeeDTO Employee)
        {
            try
            {
                var existingEmployee = await _context.Employees.Where(x => x.EmpUuid == Employee.EmpUuid).FirstOrDefaultAsync();
                if (existingEmployee == null)
                {
                    throw new Exception("Employee not found");
                }

                existingEmployee.FirstName = Employee.FirstName;
                existingEmployee.LastName = Employee.LastName;
                existingEmployee.Email = Employee.Email;
                existingEmployee.ContactNo = Employee.ContactNo;

                await _context.SaveChangesAsync();

                var updatedEmployeeDto = new EmployeeDTO
                {
                    EmpUuid = existingEmployee.EmpUuid,
                    FirstName = existingEmployee.FirstName,
                    LastName = existingEmployee.LastName,
                    Email = existingEmployee.Email,
                    ContactNo = existingEmployee.ContactNo
                };

                return updatedEmployeeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the employee.");
                throw;
            }
        }
    }
}
