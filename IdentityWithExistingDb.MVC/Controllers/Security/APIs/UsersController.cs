using IdentityWithExistingDb.Core.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWithExistingDb.MVC.Controllers.Security.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string userId)
        {
            User user = userId != null ? await _userManager.FindByIdAsync(userId) : null;

            if(user == null)
                return NotFound();

            var resutl = await _userManager.DeleteAsync(user);

            if(!resutl.Succeeded)
                return StatusCode(500, "Something went wrong please try again or call the administrator!");

            return Ok();
        }
    }
}
