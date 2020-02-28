using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
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
        readonly IRoleRepository _roleRep;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
                                IRoleRepository roleRep)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRep = roleRep;
        }

        [HttpPost("SetAsUser")]
        public async Task<IActionResult> SetAsUser(string userId)
        {
            await _roleRep.SetAsUser(userId);
            return NoContent();
        }

        [HttpPost("SetAsManager")]
        public async Task<IActionResult> SetAsManager(string userId)
        {
            await _roleRep.SetAsManager(userId);
            return Ok();
        }

        [HttpPost("SetAsAdmin")]
        public async Task<IActionResult> SetAsAdmin(string userId)
        {
            await _roleRep.SetAsAdmin(userId);
            return NoContent();
        }

        [HttpPost("AddNewRole")]
        public async Task<IActionResult> Create(string name)
        {
            await _roleManager.CreateAsync(new IdentityRole(name));
            return NoContent();
        }
        
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string name)
        {
            await _roleManager.DeleteAsync(_roleManager.Roles.Where(r => r.Name == name).FirstOrDefault());
            return NoContent();
        }
    }
}