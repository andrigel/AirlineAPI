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

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserModel>());
            var mapper = new Mapper(configuration);
            return Ok(mapper.Map<ApplicationUser, UserModel>(user));
        }
    }
}
