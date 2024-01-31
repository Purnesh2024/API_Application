using API_Application.Application.DTOs;
using API_Application.Infrastructure;
using API_Application.Models;
using MediatR;

namespace API_Application.Application.Commands.CreateAddress
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        public CreateAddressHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressDTO> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            var address = new AddressDTO()
            {
                AddressUuid = Guid.NewGuid(),
                AddressLine1 = command.AddressLine1,
                AddressLine2 = command.AddressLine2,
                Landmark = command.Landmark,
                City = command.City,
                State = command.State,
                Country = command.Country,
                UserId = command.UserId,
            };

            var addedAddress = await _addressRepository.AddAddressAsync(address);
            if(addedAddress != null)
            {
                return addedAddress;
            }
            else
            {
                throw new Exception("Failed to add the ADDRESS");
            }
        }
    }
}
