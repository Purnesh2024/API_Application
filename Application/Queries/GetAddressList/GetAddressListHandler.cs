using API_Application.Application.DTOs;
using API_Application.Infrastructure.AddressRepository;
using AutoMapper;
using MediatR;
using API_Application.Middleware;

namespace API_Application.Application.Queries.GetAddressList
{
    public class GetAddressListHandler : IRequestHandler<GetAddressListQuery, List<AddressWithoutEmpUuidDTO>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAddressListHandler> _logger;

        public GetAddressListHandler(IAddressRepository addressRepository, IMapper mapper, ILogger<GetAddressListHandler> logger)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AddressWithoutEmpUuidDTO>> Handle(GetAddressListQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var addresses = await _addressRepository.GetAddressListAsync(query.Limit, query.Offset, query.Search);
                return _mapper.Map<List<AddressWithoutEmpUuidDTO>>(addresses);
            }
            catch (NoContentException ex)
            {
                _logger.LogWarning(ex, "Address list is empty.");
                throw new NotFoundException("Address list is empty !!!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling the GetAddressListQuery.");
                throw;
            }
        }
    }
}
