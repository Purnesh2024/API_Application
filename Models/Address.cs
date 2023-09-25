using System.ComponentModel.DataAnnotations.Schema;

namespace API_Application.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
