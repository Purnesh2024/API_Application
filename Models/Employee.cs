namespace API_Application.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public Guid EmpUuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public List<Address>? Addresses { get; set; }
    }
}
