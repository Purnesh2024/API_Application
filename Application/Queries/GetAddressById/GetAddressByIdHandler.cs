using API_Application.Application.DTOs;
using API_Application.Infrastructure;
using MediatR;


namespace API_Application.Application.Queries.GetAddressById
{
    public class GetAddressByIdHandler : IRequestHandler<GetAddressByIdQuery, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddressByIdHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<AddressDTO> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
        {
            return await _addressRepository.GetAddressByIdAsync(query.AddressUuid);
        }
    }
}
