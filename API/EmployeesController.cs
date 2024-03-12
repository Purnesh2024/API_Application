using API_Application.Application.Commands.CreateEmployee;
using API_Application.Application.Commands.DeleteEmployee;
using API_Application.Application.Commands.UpdateEmployee;
using API_Application.Application.Queries.GetEmployeeById;
using API_Application.Application.Queries.GetEmployeeList;
using API_Application.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API_Application.Identity;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeeListAsync(int limit = 10, int offset = 0, string search = "Search")
        {
            var employees = await _mediator.Send(new GetEmployeeListQuery { Limit = limit, Offset = offset, Search = search });
            return Ok(employees);
        }

        [Authorize(Policy = "CombinedPolicy")]
        [HttpGet("{empUuid}")]
        [ActionName(nameof(GetEmployeeByIdAsync))]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeByIdAsync(Guid empUuid)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery() { EmpUuid = empUuid });

            if (employee == null)
            {
                return NotFound("Employee not found."); // 404 Not Found
            }
            return Ok(employee); // 200 OK
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> AddEmployeeAsync(CreateEmployeeRequest request)
        {
            var employee = await _mediator.Send(new CreateEmployeeCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.ContactNo,
                request.Addresses));
            return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { empUuid = employee.EmpUuid }, employee); // 201 Created
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{empUuid}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            var isEmployeeUpdated = await _mediator.Send(new UpdateEmployeeCommand(
                request.EmpUuid,
                request.FirstName,
                request.LastName,
                request.Email,
                request.ContactNo
            ));

            if (isEmployeeUpdated == null)
            {
                return NotFound("Employee not found."); // 404 Not Found
            }
            return Accepted(isEmployeeUpdated); // 202 Accepted
        }

        [Authorize(Policy = "ManagerPolicy")]
        [HttpDelete("{empUuid}")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(Guid empUuid)
        {
            var deletedEmployee = await _mediator.Send(new DeleteEmployeeCommand() { EmpUuid = empUuid });

            if (deletedEmployee == null)
            {
                return NotFound("Employee not found."); // 404 Not Found
            }
            return NoContent(); // 204 No Content
        }
    }
}
