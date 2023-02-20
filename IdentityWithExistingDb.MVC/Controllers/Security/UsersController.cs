using AutoMapper;
using IdentityWithExistingDb.Core.Models.Security;
using IdentityWithExistingDb.Core.ViewModels.General;
using IdentityWithExistingDb.Core.ViewModels.Security;
using IdentityWithExistingDb.Ef.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IdentityWithExistingDb.MVC.Controllers.Security
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<UsersViewModel> users = await _userManager.Users.Select(u =>
            new UsersViewModel()
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age,
                PhoneNumber = u.PhoneNumber,
                Username = u.UserName,
                Email = u.Email,
                Roles = _userManager.GetRolesAsync(u).Result
            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Add()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            AddUserViewModel model = new AddUserViewModel()
            {
                Roles = roles.Select(u => new CheckBoxViewModel { Value = u.Id, DisplayName = u.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                ModelState.AddModelError("Email", "Email Already Exist!");

            if (await _userManager.FindByNameAsync(model.Username) != null)
                ModelState.AddModelError("Username", "Username Already Exist!");

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == model.PhoneNumber))
                ModelState.AddModelError("PhoneNumber", "PhoneNumber Already Exist!");

            if (!model.Roles.Any(r => r.IsChecked))
                ModelState.AddModelError("Roles", "Please select one role at least!");

            if (!ModelState.IsValid)
                return View(model);

            User newUser = _mapper.Map<User>(model);
            newUser.EmailConfirmed = true;

            //User newUser = new User
            //{
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //    Email = model.Email,
            //    UserName = model.Username,
            //    Age = model.Age,
            //    PhoneNumber = model.PhoneNumber,
            //    EmailConfirmed = true
            //};

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("Roles", error.Description);

                return View(model);
            }

            await _userManager.AddToRolesAsync(newUser, model.Roles.Where(r => r.IsChecked).Select(role => role.DisplayName).ToList());

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();
            
            var roles =await _roleManager.Roles.ToListAsync();

            UpdateUserViewModel model = new UpdateUserViewModel
            {
                Id= userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.Select(r => new CheckBoxViewModel { Value = r.Id, DisplayName= r.Name, IsChecked = _userManager.IsInRoleAsync(user, r.Name).Result }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateUserViewModel model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);

            if (user == null) 
                return NotFound();

            if (await _userManager.FindByEmailAsync(model.Email) != null && user.Email != model.Email)
                ModelState.AddModelError("Email", "Email Already Exist!");

            if (await _userManager.FindByNameAsync(model.Username) != null && user.UserName != model.Username)
                ModelState.AddModelError("Username", "Username Already Exist!");

            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == model.PhoneNumber) && user.PhoneNumber != model.PhoneNumber)
                ModelState.AddModelError("PhoneNumber", "Phone Number Already Exist!");

            if (!model.Roles.Any(r => r.IsChecked))
                ModelState.AddModelError("Roles", "Please select one role at least!");

            if (!ModelState.IsValid)
                return View(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;
            user.Age = model.Age;
            user.PhoneNumber = model.PhoneNumber;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user);
            foreach (CheckBoxViewModel item in model.Roles)
            {
                if (!item.IsChecked && userRoles.Any(ur => ur == item.DisplayName))
                    await _userManager.RemoveFromRoleAsync(user, item.DisplayName);

                else if (item.IsChecked && !userRoles.Any(ur => ur == item.DisplayName))
                    await _userManager.AddToRoleAsync(user, item.DisplayName);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
