using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Queries.GetAddressList
{
    public class GetAddressListQuery : IRequest<List<AddressDTO>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
