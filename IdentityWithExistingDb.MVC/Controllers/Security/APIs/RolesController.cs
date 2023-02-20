using IdentityWithExistingDb.Core.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityWithExistingDb.MVC.Controllers.Security.APIs
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string roleId)
        {
            IdentityRole role = roleId != null ? await _roleManager.FindByIdAsync(roleId) : null;

            if (role == null)
                return NotFound();

            var resutl = await _roleManager.DeleteAsync(role);

            if (!resutl.Succeeded)
                return StatusCode(500, "Something went wrong please try again or call the administrator!");

            return Ok();
        }
    }
}
