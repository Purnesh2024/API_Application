namespace API_Application.Application.DTOs
{
    public class AddressDTO
    {
        public Guid AddressUuid { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int UserId { get; set; }
    }
}
