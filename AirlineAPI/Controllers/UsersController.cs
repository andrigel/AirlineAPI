using AutoMapper;
using AutoMapper.Models;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using Services.Implementations;
using Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRep;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserRepository userRep, UserManager<ApplicationUser> userManager)
        {
            _userRep = userRep;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRep.GetUsersAll());
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UserModel u)
        {
            return Ok(await _userRep.ModifyUser(u));
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await _userRep.GetUser(User.Claims.ToList()[1].Value));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok(await _userManager.DeleteAsync(await _userManager.FindByIdAsync(userId)));
        }

        [Authorize(Roles = "user")]
        [HttpDelete("DeleteCurrentUser")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            return Ok(await _userManager.DeleteAsync(await _userManager.FindByIdAsync(User.Claims.ToList()[1].Value)));
        }
    }
}
