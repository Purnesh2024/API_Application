using API_Application.Application.DTOs;
using API_Application.Infrastructure.AddressRepository;
using API_Application.Middleware;
using MediatR;

namespace API_Application.Application.Commands.DeleteAddress
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<DeleteAddressHandler> _logger;

        public DeleteAddressHandler(IAddressRepository addressRepository, ILogger<DeleteAddressHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<AddressDTO> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(command.AddressUuid);
                if (address == null)
                {
                    throw new NotFoundException("Address Not Found !!!");
                }

                return await _addressRepository.DeleteAddressAsync(address.AddressUuid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the DeleteAddressCommand.");
                throw;
            }
        }
    }
}
