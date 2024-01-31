using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<AddressDTO>
    {
        public Guid AddressUuid { get; set; }
    }
}
