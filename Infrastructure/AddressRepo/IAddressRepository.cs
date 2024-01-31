using API_Application.Application.DTOs;
using API_Application.Models;

namespace API_Application.Infrastructure
{
    public interface IAddressRepository
    {
        public Task<List<AddressDTO>> GetAddressListAsync(int limit, int offset);
        public Task<AddressDTO> GetAddressByIdAsync(Guid AddressUuid);
        public Task<AddressDTO> AddAddressAsync(AddressDTO address);
        public Task<AddressDTO> UpdateAddressAsync(AddressDTO address);
        public Task<AddressDTO> DeleteAddressAsync(Guid AddressUuid);        
    }
}
