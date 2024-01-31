using API_Application.Application.DTOs;
using API_Application.Infrastructure.Context;
using API_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Application.Infrastructure.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly APIContext _context;
        public AddressRepository(APIContext context)
        {
            _context = context;
        }

        public async Task<AddressDTO> AddAddressAsync(AddressDTO address)
        {
            var newAddress = new Address
            {
                AddressUuid = Guid.NewGuid(),
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                Country = address.Country,
                UserId = address.UserId,
            };

            var result = _context.Addresses.Add(newAddress);
            await _context.SaveChangesAsync();

            var addedAddressDto = new AddressDTO
            {
                AddressUuid = result.Entity.AddressUuid,
                AddressLine1 = result.Entity.AddressLine1,
                AddressLine2 = result.Entity.AddressLine2,
                Landmark = result.Entity.Landmark,
                City = result.Entity.City,
                State = result.Entity.State,
                Country = result.Entity.Country,
                UserId = result.Entity.UserId,
            };

            return addedAddressDto;
        }

        public async Task<AddressDTO> DeleteAddressAsync(Guid AddressUuid)
        {
            var addressToDelete = _context.Addresses.FirstOrDefault(x => x.AddressUuid == AddressUuid);

            if (addressToDelete != null)
            {
                _context.Addresses.Remove(addressToDelete);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<AddressDTO> GetAddressByIdAsync(Guid AddressUuid)
        {
            var address = await _context.Addresses.Where(x => x.AddressUuid == AddressUuid).FirstOrDefaultAsync();

            if (address == null)
            {
                return null;
            }

            var addressDto = new AddressDTO
            {
                AddressUuid = address.AddressUuid,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                Country = address.Country,
                UserId = address.UserId,
            };
            return addressDto;
        }

        public async Task<List<AddressDTO>> GetAddressListAsync(int limit, int offset)
        {
            var address = await _context.Addresses.Skip(offset).Take(limit).ToListAsync();

            var addressList = address.Select(address => new AddressDTO
            {
                AddressUuid = address.AddressUuid,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                Country = address.Country,
                UserId = address.UserId,
            }).ToList();
            return addressList;
        }

        public async Task<AddressDTO> UpdateAddressAsync(AddressDTO address)
        {
            var existingAddress = await _context.Addresses.FindAsync(address.AddressUuid);

            if (existingAddress == null)
            {
                throw new Exception("ADDRESS not found");
            }

            existingAddress.AddressLine1 = address.AddressLine1;
            existingAddress.AddressLine2 = address.AddressLine2;
            existingAddress.Landmark = address.Landmark;
            existingAddress.City = address.City;
            existingAddress.State = address.State;
            existingAddress.Country = address.Country;
            existingAddress.UserId = address.UserId;

            _context.Addresses.Update(existingAddress);
            await _context.SaveChangesAsync();

            var updatedAddressDto = new AddressDTO
            {
                AddressUuid = existingAddress.AddressUuid,
                AddressLine1 = existingAddress.AddressLine1,
                AddressLine2 = existingAddress.AddressLine2,
                Landmark = existingAddress.Landmark,
                City = existingAddress.City,
                State = existingAddress.State,
                Country = existingAddress.Country,
                UserId = existingAddress.UserId,
            };

            return updatedAddressDto;
        }
    }
}
