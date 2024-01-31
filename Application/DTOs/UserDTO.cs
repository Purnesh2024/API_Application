namespace API_Application.Application.DTOs
{
    public class UserDTO
    {
        public Guid UserUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<AddressDTO>? Addresses { get; set; }
    }
}
