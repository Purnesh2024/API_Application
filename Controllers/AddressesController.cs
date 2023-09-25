using API_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly APIContext _dbContext;
        public AddressesController(APIContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddress()
        {
            var addresses = await _dbContext.Addresses.ToListAsync();

            return addresses;
        }

        // GET: api/addresses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // POST: api/addresses
        [HttpPost]
        public async Task<ActionResult<Address>> AddAddress(Address address)
        {
            _dbContext.Addresses.Add(address);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
        }

        // PUT: api/addresses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, Address updatedAddress)
        {
            if (updatedAddress == null)
            {
                return BadRequest("Invalid data");
            }

            var existingAddress = await _dbContext.Addresses.FindAsync(id);
            if (existingAddress == null)
            {
                return NotFound();
            }

            // Update the existing address properties with the new data
            existingAddress.AddressLine1 = updatedAddress.AddressLine1;
            existingAddress.AddressLine2 = updatedAddress.AddressLine2;
            existingAddress.Landmark = updatedAddress.Landmark;
            existingAddress.City = updatedAddress.City;
            existingAddress.State = updatedAddress.State;
            existingAddress.Country = updatedAddress.Country;            

            // Save changes to the database
            await _dbContext.SaveChangesAsync();
            return Ok(existingAddress);
        }

        // DELETE: api/addresses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _dbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }
            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
