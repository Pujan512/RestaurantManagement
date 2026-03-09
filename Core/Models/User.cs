using Microsoft.AspNetCore.Identity;

namespace RestaurantManagement.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = "";
        public string Status { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
