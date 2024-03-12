using API_Application.Application.DTOs;
using MediatR;

namespace API_Application.Application.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<AddressDTO>
    {
        public Guid AddressUuid { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public UpdateAddressCommand(string? addressLine1, string? addressLine2, string? landmark, string? city, string? state, string? country)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Landmark = landmark;
            City = city;
            State = state;
            Country = country;
        }
    }
}
