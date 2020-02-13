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

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserModel>());
            var mapper = new Mapper(configuration);
            return Ok(mapper.Map<ApplicationUser, UserModel>(user));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if ((userId == "0") && (User.Identity.IsAuthenticated)) userId = User.Claims.ToList()[1].Value;
            var u = await _userRep.GetUser(userId, true);
            if (u == null) return BadRequest();
            return Ok(await _userManager.DeleteAsync(u));
        }

        [Authorize(Roles = "user")]
        [HttpDelete("DeleteCurrentUser")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var userId = User.Claims.ToList()[1].Value;
            var u = await _userRep.GetUser(userId, true);
            return Ok(await _userManager.DeleteAsync(u));
        }
    }
}
