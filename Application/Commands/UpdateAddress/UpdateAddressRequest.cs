namespace API_Application.Application.Commands.UpdateAddress
{
    public class UpdateAddressRequest
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int UserId { get; set; }
    }
}
