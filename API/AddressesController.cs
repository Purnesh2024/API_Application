using Microsoft.AspNetCore.Mvc;
using MediatR;
using API_Application.Application.Queries.GetAddressList;
using API_Application.Application.Queries.GetAddressById;
using API_Application.Application.Commands.CreateAddress;
using API_Application.Application.Commands.UpdateAddress;
using API_Application.Application.Commands.DeleteAddress;
using API_Application.Application.DTOs;

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
        public async Task<ActionResult<List<AddressDTO>>> GetAddressListAsync(int limit = 10, int offset = 0, string search = "Search")
        {
            var addresses = await _mediator.Send(new GetAddressListQuery { Limit = limit, Offset = offset, Search = search });
            return Ok(addresses);
        }

        [HttpGet("{addressUuid}")]
        [ActionName(nameof(GetAddressByIdAsync))]
        public async Task<ActionResult<AddressDTO>> GetAddressByIdAsync(Guid addressUuid)
        {
            var addresses = await _mediator.Send(new GetAddressByIdQuery() { AddressUuid = addressUuid });

            if (addresses == null)
            {
                return NotFound();
            }
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> AddAddressAsync(CreateAddressRequest request)
        {
            var address = await _mediator.Send(new CreateAddressCommand(
                request.AddressLine1,
                request.AddressLine2,
                request.Landmark,
                request.City,
                request.State,
                request.Country,
                request.EmpUuid));
            return CreatedAtAction(nameof(GetAddressByIdAsync), new { addressUuid = address.AddressUuid }, address);
        }

        [HttpPut("{AddressUuid}")]
        public async Task<ActionResult<AddressDTO>> UpdateAddressAsync(UpdateAddressRequest request)
        {
            var isAddressUpdated = await _mediator.Send(new UpdateAddressCommand(
                request.AddressLine1,
                request.AddressLine2,
                request.Landmark,
                request.City,
                request.State,
                request.Country));

            if (isAddressUpdated == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Accepted(isAddressUpdated);
        }

        [HttpDelete("{AddressUuid}")]
        public async Task<ActionResult<AddressDTO>> DeleteAddressAsync(Guid addressUuid)
        {
            var deletedAddress = await _mediator.Send(new DeleteAddressCommand() { AddressUuid = addressUuid });

            if (deletedAddress == null)
            {
                return NotFound(); // 404 Not Found
            }
            return NoContent(); // 204 No Content            
        }
    }
}
