namespace API_Application.Application.DTOs
{
    public class EmployeeWithoutEmpUuidDTO
    {
        public Guid EmpUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<AddressWithoutEmpUuidDTO>? Addresses { get; set; }
    }
}
