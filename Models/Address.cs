using System.ComponentModel.DataAnnotations.Schema;

namespace API_Application.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public Guid AddressUuid { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
