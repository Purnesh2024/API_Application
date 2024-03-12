using API_Application.Application.DTOs;
using API_Application.Infrastructure.Context;
using API_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Application.Infrastructure.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly APIContext _context;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(APIContext context, ILogger<AddressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AddressDTO> AddAddressAsync(AddressDTO address)
        {
            try
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
                    EmpUuid = address.EmpUuid,
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
                    EmpUuid = result.Entity.EmpUuid,
                };

                return addedAddressDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding address.");
                throw;
            }
        }

        public async Task<AddressDTO> DeleteAddressAsync(Guid addressUuid)
        {
            try
            {
                var addressToDelete = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressUuid == addressUuid);

                if (addressToDelete != null)
                {
                    _context.Addresses.Remove(addressToDelete);
                    await _context.SaveChangesAsync();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting address.");
                throw;
            }
        }

        public async Task<AddressDTO> GetAddressByIdAsync(Guid addressUuid)
        {
            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressUuid == addressUuid);

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
                    EmpUuid = address.EmpUuid,
                };
                return addressDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting address by ID.");
                throw;
            }
        }

        public async Task<List<AddressWithoutEmpUuidDTO>> GetAddressListAsync(int limit, int offset, string search)
        {
            try
            {
                var query = _context.Addresses.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(address =>
                        address.AddressLine1.Contains(search) ||
                        address.AddressLine2.Contains(search) ||
                        address.Landmark.Contains(search) ||
                        address.City.Contains(search) ||
                        address.State.Contains(search) ||
                        address.Country.Contains(search));
                }

                var addresses = await query.Skip(offset).Take(limit).ToListAsync();

                var addressList = addresses.Select(address => new AddressWithoutEmpUuidDTO
                {
                    AddressUuid = address.AddressUuid,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    Landmark = address.Landmark,
                    City = address.City,
                    State = address.State,
                    Country = address.Country
                }).ToList();

                return addressList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting address list.");
                throw;
            }
        }

        public async Task<AddressDTO> UpdateAddressAsync(AddressDTO address)
        {
            try
            {
                var existingAddress = await _context.Addresses.FindAsync(address.AddressUuid);

                if (existingAddress == null)
                {
                    throw new Exception("Address not found");
                }

                existingAddress.AddressLine1 = address.AddressLine1;
                existingAddress.AddressLine2 = address.AddressLine2;
                existingAddress.Landmark = address.Landmark;
                existingAddress.City = address.City;
                existingAddress.State = address.State;
                existingAddress.Country = address.Country;

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
                    Country = existingAddress.Country
                };

                return updatedAddressDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating address.");
                throw;
            }
        }
    }
}
