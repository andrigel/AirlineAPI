using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Implementations
{
    public class EFAccountRepository : IAccountRepository
    {
        private readonly EFDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EFAccountRepository(EFDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Register(RegisterViewModel model)
        {
            if (model.Password == model.PasswordConfirm)
                throw new Exception("Password confirm failed");

            await _userManager.CreateAsync(new ApplicationUser { Email = model.Email, Year = model.Year, UserName = model.UserName }, model.Password);
            var u = await _userManager.FindByEmailAsync(model.Email);
            await _userManager.AddToRoleAsync(u, "user");
        }
    }
}
