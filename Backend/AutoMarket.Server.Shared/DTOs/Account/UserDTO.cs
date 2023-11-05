namespace AutoMarket.Server.Shared.DTOs.Account
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string JWT { get; set; }
    }
}
