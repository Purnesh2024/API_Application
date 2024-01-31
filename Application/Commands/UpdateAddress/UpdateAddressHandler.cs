using API_Application.Application.DTOs;
using API_Application.Infrastructure;
using MediatR;

namespace API_Application.Application.Commands.UpdateAddress
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        public UpdateAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<AddressDTO> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAddressByIdAsync(command.AddressUuid);
            {
                if (address == null)
                {
                    throw new Exception("ADDRESS Not Found ");
                }

                address.AddressLine1 = command.AddressLine1;
                address.AddressLine2 = command.AddressLine2;
                address.Landmark = command.Landmark;
                address.City = command.City;
                address.State = command.State;
                address.Country = command.Country;
                address.UserId = command.UserId;

                await _addressRepository.UpdateAddressAsync(address);

                return address;
            }            
        }
    }
}
