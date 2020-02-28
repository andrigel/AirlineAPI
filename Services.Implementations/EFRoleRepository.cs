using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFRoleRepository : IRoleRepository
    {
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<ApplicationUser> _userManager;

        public EFRoleRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SetAsAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await DeleteAllUserRoles(user);

            await _userManager.AddToRoleAsync(user, "admin");
        }

        public async Task SetAsManager(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await DeleteAllUserRoles(user);

            await _userManager.AddToRoleAsync(user, "manager");
        }
        public async Task SetAsUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await DeleteAllUserRoles(user);

            await _userManager.AddToRoleAsync(user, "user");
        }

        private async Task DeleteAllUserRoles(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }
    }
}
