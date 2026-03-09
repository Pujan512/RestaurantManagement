using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.BLL.Services;
using RestaurantManagement.Core.Constants;
using RestaurantManagement.Core.ViewModels.Account;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    public class AccountController(IAccountServices accountServices, ILogger<AccountController> logger) : Controller
    {
        private readonly IAccountServices _accountServices = accountServices;
        private readonly ILogger<AccountController> _logger = logger;
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all users");
                var users = await _accountServices.GetAllUsers();
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex.Message);
                RedirectToAction("Error", "Home");
            }
            return NoContent();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName,MiddleName,LastName,Status,Role,Shift,Email,Password,ConfirmPassword")]RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Registering new user");
                    var result = await _accountServices.Register(registerVM);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex.Message);
                }

            }
            return View(registerVM);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FirstName,MiddleName,LastName,Status,Role,Shift,Email,Password,ConfirmPassword")] AddUserVM addUserVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating new user");
                    var result= await _accountServices.AddUser(addUserVM);
                    if (result.Succeeded) return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex.Message);
                }
            }

            return View(addUserVM);
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                _logger.LogInformation("Fetching user {userId}", id);
                var user = await _accountServices.GetUser(id);
                if (user == null) return NotFound();

                return View(_accountServices.EditVMMapper(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,MiddleName,LastName,Status,Role")] EditUserVM editUserVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Editing user {userId}", id);
                    bool success = await _accountServices.EditUser(id, editUserVM);
                    if (success) return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(editUserVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Logging In");
                    var result = await _accountServices.Login(loginVM);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Home");
                    }

                    ModelState.AddModelError("", "Invalid credentials");
                    return View(loginVM);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(loginVM);
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogoutConfirmed()
        {
            try
            {
                _logger.LogInformation("Logging Out");
                bool success = await _accountServices.Logout();
                if (success) return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Error", "Home");

        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _logger.LogInformation("Fetching user {userId}", id);
                var user = await _accountServices.GetUser(id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                _logger.LogInformation("Deleting user {userId}", id);
                bool success = await _accountServices.DeleteUser(id);
                if (success) return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Error", "Home");

        }
    }

}
