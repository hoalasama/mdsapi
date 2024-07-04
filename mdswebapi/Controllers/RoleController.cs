using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mdswebapi.Dtos;
using mdswebapi.Models;
using System.Threading.Tasks;
using mdswebapi.Dtos.Account;

namespace mdswebapi.Controllers
{
    /*[Authorize(Roles = "Admin")]*/
    [ApiController]
    [Route("api/Role")]
    public class RoleManagementController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagementController(UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("update-role")]
        public async Task<IActionResult> UpdateUserRole([FromBody] RoleUpdateDto roleUpdateDto)
        {
            var user = await _userManager.FindByIdAsync(roleUpdateDto.UserId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            if (!await _roleManager.RoleExistsAsync(roleUpdateDto.NewRole))
            {
                return BadRequest(new { Message = "Role does not exist" });
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest(new { Message = "Failed to remove user roles", Errors = removeResult.Errors });
            }

            var addResult = await _userManager.AddToRoleAsync(user, roleUpdateDto.NewRole);
            if (!addResult.Succeeded)
            {
                return BadRequest(new { Message = "Failed to add user to role", Errors = addResult.Errors });
            }

            return Ok(new { Message = "User role updated successfully" });
        }
        [HttpPost("update-all-to-phar-except-admin")]
        public async Task<IActionResult> UpdateAllUserRolesToPharExceptAdmin()
        {
            var users = _userManager.Users.ToList();
            var updatedUsers = new List<string>();

            foreach (var user in users)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (!currentRoles.Contains("Admin"))
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        return BadRequest(new { Message = $"Failed to remove roles from user {user.UserName}", Errors = removeResult.Errors });
                    }

                    var addResult = await _userManager.AddToRoleAsync(user, "Phar");
                    if (!addResult.Succeeded)
                    {
                        return BadRequest(new { Message = $"Failed to add user {user.UserName} to Phar role", Errors = addResult.Errors });
                    }

                    updatedUsers.Add(user.UserName);
                }
            }

            return Ok(new { Message = "All non-admin user roles updated to Phar successfully", UpdatedUsers = updatedUsers });
        }

    }
}
