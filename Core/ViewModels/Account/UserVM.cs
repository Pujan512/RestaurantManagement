using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Account
{
    public class UserVM
    {
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = "";
        public string Status { get; set; } = "";
        public string Role { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
