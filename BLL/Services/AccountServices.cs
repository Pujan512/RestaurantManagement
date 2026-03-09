using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Utilities;
using RestaurantManagement.Core.ViewModels.Account;
using RestaurantManagement.Core.ViewModels.Schedule;

namespace RestaurantManagement.BLL.Services
{
    public class AccountServices(UserManager<User> userManager,
        SignInManager<User> signInManager,
        Utils utils,
        IScheduleServices scheduleServices,
        IMapper mapper) : IAccountServices
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly Utils _utils = utils;
        private readonly IScheduleServices _scheduleServices = scheduleServices;
        public async Task<SignInResult> Login(LoginVM loginVM)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (user == null) return SignInResult.Failed;

                if (user.Status == "Pending")
                    throw new Exception("User not approved yet.");
                if (user.Status == "Disabled")
                    throw new Exception("Your account has been disabled. Please contact the manager.");

                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, lockoutOnFailure: false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                throw new Exception("Logout failed");
            }
        }

        public async Task<IdentityResult> Register(RegisterVM registerVM)
        {
            try
            {
                var result = await AddUser(_mapper.Map<AddUserVM>(registerVM));
                return result;
            }
            catch
            {
                throw;
            }

        }

        public async Task<UserVM?> GetUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                return _mapper.Map<UserVM>(user);
            }
            catch
            {
                throw new Exception("Unable to get user from db");
            }
        }

        public async Task<IEnumerable<UserVM>> GetAllUsers()
        {
            try
            {
                var user = await _utils.GetActiveUser();
                var users = await _userManager.Users.Where(u => u.Id != user!.Id).ToListAsync();
                return _mapper.Map<IEnumerable<UserVM>>(users);

            }
            catch
            {
                throw new Exception("Unable to get users from db");
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) throw new Exception("User not found");
            try
            {
                var result = await _userManager.DeleteAsync(user);
                return true;
            }
            catch
            {
                throw new Exception("Unable to delete user");
            }
        }

        public async Task<IdentityResult> AddUser(AddUserVM addUserVM)
        {
            var user = await _userManager.FindByEmailAsync(addUserVM.Email);
            if (user != null) throw new Exception("Email is already taken");

            var newUser = new User
            {
                UserName = addUserVM.Email,
                FirstName = addUserVM.FirstName.Substring(0, 1).ToUpper() + addUserVM.FirstName.Substring(1).ToLower(),
                MiddleName = addUserVM.MiddleName?.Substring(0, 1).ToUpper() + addUserVM.MiddleName?.Substring(1).ToLower(),
                LastName = addUserVM.LastName.Substring(0, 1).ToUpper() + addUserVM.LastName.Substring(1).ToLower(),
                Email = addUserVM.Email,
                Role = addUserVM.Role,
                Status = addUserVM.Status
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, addUserVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, addUserVM.Role);
                    bool success = await _scheduleServices.AddScheduleAsync(new AddScheduleVM() { UserId = newUser.Id });
                    if (!success) throw new Exception("Couldn't add schedule for the user");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> EditUser(string id, EditUserVM editUserVM)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            var removeResult = await _userManager.RemoveFromRoleAsync(user, user.Role);
            if (removeResult.Succeeded)
            {
                user.FirstName = editUserVM.FirstName.Substring(0, 1).ToUpper() + editUserVM.FirstName.Substring(1).ToLower();
                user.MiddleName = editUserVM.MiddleName?.Substring(0, 1).ToUpper() + editUserVM.MiddleName?.Substring(1).ToLower();
                user.LastName = editUserVM.LastName.Substring(0, 1).ToUpper() + editUserVM.LastName.Substring(1).ToLower();
                user.Status = editUserVM.Status;
                user.Role = editUserVM.Role;

                try
                {
                    await _userManager.UpdateAsync(user);
                    await _userManager.AddToRoleAsync(user, editUserVM.Role);
                }
                catch
                {
                    throw new Exception("Unable to update user");
                }
            }
            return true;
        }

        public EditUserVM EditVMMapper(UserVM userVM)
        {
            return new EditUserVM()
            {
                FirstName = userVM.FirstName,
                MiddleName = userVM.MiddleName,
                LastName = userVM.LastName,
                Status = userVM.Status,
                Role = userVM.Role
            };
        }
    }
}
