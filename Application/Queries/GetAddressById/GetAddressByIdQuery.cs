using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Queries.GetAddressById
{
    public class GetAddressByIdQuery : IRequest<AddressDTO>
    {
        public Guid AddressUuid { get; set; }
    }
}
