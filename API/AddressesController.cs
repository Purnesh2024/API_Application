using Microsoft.AspNetCore.Mvc;
using MediatR;
using API_Application.Application.Queries.GetAddressList;
using API_Application.Application.Queries.GetAddressById;
using API_Application.Application.Commands.CreateAddress;
using API_Application.Application.Commands.UpdateAddress;
using API_Application.Application.Commands.DeleteAddress;
using API_Application.Application.DTOs;
using API_Application.Application.Commands.DeleteUser;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddressesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<List<AddressDTO>>> GetAddressListAsync(int limit = 10, int offset = 0)
        {
            try
            {
                var addresses = await _mediator.Send(new GetAddressListQuery { Limit = limit, Offset = offset });
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("{AddressUuid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<AddressDTO>> GetAddressByIdAsync(Guid addressUuid)
        {
            try
            {
                var addresses = await _mediator.Send(new GetAddressByIdQuery() { AddressUuid = addressUuid });

                if(addresses == null)
                {
                    return NotFound();
                }
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<AddressDTO>> AddAddressAsync(CreateAddressRequest request)
        {
            try
            {
                var address = await _mediator.Send(new CreateAddressCommand(
                    request.AddressLine1,
                    request.AddressLine2,
                    request.Landmark,
                    request.City,
                    request.State,
                    request.Country,
                    request.UserId));
                return CreatedAtAction(nameof(GetAddressByIdAsync), new {addressUuid = address.AddressUuid}, address);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut("{AddressUuid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<AddressDTO>> UpdateAddressAsync(UpdateAddressRequest request)
        {
            try
            {
                var isAddressUpdated = await _mediator.Send(new UpdateAddressCommand(
                    request.AddressLine1,
                    request.AddressLine2,
                    request.Landmark,
                    request.City,
                    request.State,
                    request.Country,
                    request.UserId));

                if (isAddressUpdated == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return Accepted(isAddressUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete("{AddressUuid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<AddressDTO>> DeleteAddressAsync(Guid addressUuid)
        {
            try
            {
                var deletedAddress = await _mediator.Send(new DeleteAddressCommand() { AddressUuid = addressUuid });

                if (deletedAddress == null)
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
