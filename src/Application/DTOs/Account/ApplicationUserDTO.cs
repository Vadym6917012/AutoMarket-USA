namespace Application.DTOs.Account
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string JWT { get; set; }
    }
}
