using API_Application.Application.DTOs;
using API_Application.Infrastructure;
using MediatR;

namespace API_Application.Application.Queries.GetAddressList
{
    public class GetAddressListHandler : IRequestHandler<GetAddressListQuery, List<AddressDTO>>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddressListHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<List<AddressDTO>> Handle(GetAddressListQuery query, CancellationToken cancellationToken)
        {
            // Fetch users based on Limit and Offset
            var addresses = await _addressRepository.GetAddressListAsync(query.Limit, query.Offset);
            return addresses.Select(address => MapToAddressDTO(address)).ToList();
        }

        public AddressDTO MapToAddressDTO(AddressDTO address)
        {
            return new AddressDTO
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                Landmark = address.Landmark,
                City = address.City,
                State = address.State,
                Country = address.Country,
                UserId = address.UserId
            };
        }
    }
}
