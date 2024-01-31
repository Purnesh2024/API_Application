using API_Application.Application.DTOs;

namespace API_Application.Models

{
    public class User
    {
        public int UserId { get; set; }
        public Guid UserUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<Address>? Addresses { get; set; }
    }
}