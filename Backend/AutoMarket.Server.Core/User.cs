using Microsoft.AspNetCore.Identity;

namespace AutoMarket.Server.Core
{
    public class User : IdentityUser
    {
        public string? FirstName {  get; set; }
        public string? LastName { get; set;}

        public virtual ICollection<Car>? Cars { get; set; }
    }
}
