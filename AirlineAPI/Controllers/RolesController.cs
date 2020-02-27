using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class RolesController : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("CreateManager")]
        [Authorize]
        public async Task<IActionResult> CreateSuperAdmin()
        {
            var user = await _userManager.FindByIdAsync(User.Claims.ToList()[1].Value);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            await _userManager.AddToRoleAsync(user, "manager");
            return Ok();
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
        
        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdminFromUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }

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
            return Ok(await _roleManager.Roles.ToListAsync());
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