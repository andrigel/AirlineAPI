using AirlineAPI.Options;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(LoginViewModel model)
        {
            var u = await _userManager.FindByEmailAsync(model.Email);
            if (u == null) return NotFound();
            var result = await _signInManager.PasswordSignInAsync(u.UserName, model.Password, false, false);
            var userRoles = await _userManager.GetRolesAsync(u);
            var userRole = userRoles.Single();

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole),
                new Claim("UserId", u.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, u.UserName)         
            };

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = "Bearer " + encodedJwt,
                userId = u.Id,
                username = u.UserName
            };
            return Json(response);
        }

        [Authorize]
        [HttpPost("getRole")]
        public async Task<IActionResult> GetAccountType()
        {
            var role = await _userManager.GetRolesAsync( await _userManager.FindByNameAsync(User.Identity.Name));
            return Ok(role.Single());
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(model.Password == model.PasswordConfirm)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser { Email = model.Email, Year = model.Year, UserName = model.UserName }, model.Password);
                if(result.Succeeded)
                {
                    var u = await _userManager.FindByNameAsync(model.UserName);
                    if(u != null)
                    {
                        await _userManager.AddToRoleAsync(u, "user");
                    }
                }
                return Ok(result.Errors);
            }
            return NoContent();
        }
    }

}