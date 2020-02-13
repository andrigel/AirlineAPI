using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("AddNewRole")]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return Ok("Role created!");
                }
                else
                {
                    return Ok("Cant create role!");
                }
            }
            return NoContent();
        }
        
        [HttpPost("CreateAdminFromUser")]
        public async Task<IActionResult> CreateAdminFromUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                await _userManager.RemoveFromRoleAsync(user, "admin");
                await _userManager.RemoveFromRoleAsync(user, "user"); 
                await _userManager.AddToRoleAsync(user, "admin");
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(_roleManager.Roles.ToList());
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string name)
        {
            IdentityRole role = _roleManager.Roles.Where(r => r.Name == name).FirstOrDefault();
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return Ok("Role Deleted");
        }
    }
}