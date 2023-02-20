using AutoMapper;
using IdentityWithExistingDb.Core.Repositories;
using IdentityWithExistingDb.Core.ViewModels.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithExistingDb.MVC.Controllers.Security
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolesRepository;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<IdentityRole> rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {            
            return View(_rolesRepository.Roles.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), _rolesRepository.Roles.ToList());

            if (_rolesRepository.RoleExistsAsync(model.RoleName.Trim()).Result)
            {
                ModelState.AddModelError("RoleName", "Role already exist!");
                return View(nameof(Index), _rolesRepository.Roles.ToList());
            }

            IdentityRole newRole = _mapper.Map<IdentityRole>(model);

            await _rolesRepository.CreateAsync(newRole);

            return RedirectToAction(nameof(Index));
        }
    }
}
