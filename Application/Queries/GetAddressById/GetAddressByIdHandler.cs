using API_Application.Application.DTOs;
using API_Application.Infrastructure.AddressRepository;
using Microsoft.Extensions.Logging;
using MediatR;
using API_Application.Middleware;

namespace API_Application.Application.Queries.GetAddressById
{
    public class GetAddressByIdHandler : IRequestHandler<GetAddressByIdQuery, AddressDTO>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<GetAddressByIdHandler> _logger;

        public GetAddressByIdHandler(IAddressRepository addressRepository, ILogger<GetAddressByIdHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<AddressDTO> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(query.AddressUuid);

                if (address != null)
                {
                    return address;
                }
                else
                {
                    throw new NotFoundException("Address not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the GetAddressByIdQuery.");
                throw;
            }
        }
    }
}
