using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.CreateAddress
{
    public class CreateAddressCommand : IRequest<AddressDTO>
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public Guid EmpUuid { get; set; }
        public CreateAddressCommand(string? addressLine1, string? addressLine2, string? landmark, string? city, string? state, string? country, Guid empUuid)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Landmark = landmark;
            City = city;
            State = state;
            Country = country;
            EmpUuid = empUuid;
        }
    }
}
