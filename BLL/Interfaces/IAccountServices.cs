using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Account;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface IAccountServices
    {
        public Task<IdentityResult> Register(RegisterVM userVM);
        public Task<SignInResult> Login(LoginVM loginVM);
        public Task<bool> Logout();
        public Task<UserVM?> GetUser(string id);
        public Task<IEnumerable<UserVM>> GetAllUsers();
        public Task<bool> DeleteUser(string id);
        public Task<IdentityResult> AddUser(AddUserVM userVM);
        public Task<bool> EditUser(string id, EditUserVM userVM);
        public EditUserVM EditVMMapper(UserVM userVM);
    }
}
