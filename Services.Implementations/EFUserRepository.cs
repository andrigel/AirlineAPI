using DataLayer;
using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Models;

namespace Services.Implementations
{
    public class EFUserRepository : IUserRepository
    {
        private EFDBContext _context;
        public EFUserRepository(EFDBContext context)
        {
            this._context = context;
        }

        public async ValueTask<ApplicationUser> GetUser(string id, bool IncludeTickets = false)
        {
            if (IncludeTickets)
            {
                var u = await _context.ApplicationsUsers.Where(u => u.Id == id).Include(u => u.Tickets).FirstOrDefaultAsync();
                return u;
            }
            else return await _context.ApplicationsUsers.FindAsync(id);
        }

        public async Task<List<UserModel>> GetUsersAll()
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserModel>());
            var mapper = new Mapper(configuration);

            List<UserModel> userModels = new List<UserModel>();

            var users = await _context.Users.ToListAsync();
            foreach (var u in users)
            {
                userModels.Add(mapper.Map<ApplicationUser, UserModel>(u));
            }
            return userModels;
        }

        public async Task<bool> PutUser(ApplicationUser u)
        {
            try
            {
                _context.Entry(u).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
