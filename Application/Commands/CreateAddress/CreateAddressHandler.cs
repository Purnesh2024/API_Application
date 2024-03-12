using API_Application.Application.DTOs;
using API_Application.Infrastructure.AddressRepository;
using API_Application.Models;
using MediatR;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API_Application.Application.Commands.CreateAddress
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<CreateAddressHandler> _logger;

        public CreateAddressHandler(IAddressRepository addressRepository, ILogger<CreateAddressHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<AddressDTO> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            try
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
                    EmpUuid = command.EmpUuid,
                };

                var addedAddress = await _addressRepository.AddAddressAsync(address);

                if (addedAddress != null)
                {
                    return addedAddress;
                }
                else
                {
                    throw new ValidationException("Failed to add the ADDRESS");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the CreateAddressCommand.");
                throw;
            }
        }
    }
}
