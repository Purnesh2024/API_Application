using API_Application.Application.DTOs;
using API_Application.Infrastructure.AddressRepository;
using API_Application.Middleware;
using Microsoft.Extensions.Logging;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API_Application.Application.Commands.UpdateAddress
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<UpdateAddressHandler> _logger;

        public UpdateAddressHandler(IAddressRepository addressRepository, ILogger<UpdateAddressHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<AddressDTO> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(command.AddressUuid);

                if (address == null)
                {
                    throw new NotFoundException("Address not found.");
                }

                address.AddressLine1 = command.AddressLine1;
                address.AddressLine2 = command.AddressLine2;
                address.Landmark = command.Landmark;
                address.City = command.City;
                address.State = command.State;
                address.Country = command.Country;

                await _addressRepository.UpdateAddressAsync(address);

                return address;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Bad Request !!!");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the UpdateAddressCommand.");
                throw;
            }
        }
    }
}
