using API_Application.Application.DTOs;
using API_Application.Infrastructure;
using MediatR;

namespace API_Application.Application.Commands.DeleteAddress
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        public DeleteAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<AddressDTO> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAddressByIdAsync(command.AddressUuid);
            if (address == null)
                return default;

            return await _addressRepository.DeleteAddressAsync(address.AddressUuid);
        }
    }
}
