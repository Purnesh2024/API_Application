using API_Application.Application.Commands.CreateUser;
using API_Application.Application.Commands.DeleteUser;
using API_Application.Application.Commands.UpdateUser;
using API_Application.Application.Queries.GetUserById;
using API_Application.Application.Queries.GetUserList;
using API_Application.Application.DTOs;
using API_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<List<UserDTO>>> GetUserListAsync(int limit = 10, int offset = 0)
        {
            try
            {
                var users = await _mediator.Send(new GetUserListQuery { Limit = limit, Offset = offset });
                return Ok(users); // 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message); // 500 Internal Server Error
            }
        }


        [HttpGet("{userUuid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(Guid userUuid)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery() { UserUuid = userUuid });

                if (user == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return Ok(user); // 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // 500 Internal Server Error
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<UserDTO>> AddUserAsync(CreateUserRequest request)
        {
            try
            {
                var user = await _mediator.Send(new CreateUserCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.ContactNo,
                    request.Addresses));

                return CreatedAtAction(nameof(GetUserByIdAsync), new { userUuid = user.UserUuid }, user); // 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message); // 400 Bad Request
            }
        }

        [HttpPut("{userUuid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<UserDTO>> UpdateUserAsync(UpdateUserRequest request)
        {
            try
            {
                var isUserUpdated = await _mediator.Send(new UpdateUserCommand(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.ContactNo,
                    request.Addresses
                ));

                if (isUserUpdated == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return Accepted(isUserUpdated); // 202 Accepted
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message); // 400 Bad Request
            }
        }

        [HttpDelete("{userUuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<UserDTO>> DeleteUser(Guid userUuid)
        {
            try
            {
                var deletedUser = await _mediator.Send(new DeleteUserCommand() { UserUuid = userUuid });

                if (deletedUser == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message); // 400 Bad Request
            }
        }
    }
}
