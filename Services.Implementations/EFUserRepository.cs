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

namespace Services.Implementations
{
    public class EFUserRepository : IUserRepository
    {
        private EFDBContext context;
        public EFUserRepository(EFDBContext context)
        {
            this.context = context;
        }

        public async ValueTask<ApplicationUser> GetUser(string id, bool IncludeTickets = false)
        {
            if (IncludeTickets)
            {
                var u = await context.ApplicationsUsers.Where(u=>u.Id == id).Include(u => u.Tickets).FirstOrDefaultAsync();
                return u;
            }
            else return await context.ApplicationsUsers.FindAsync(id);
        }

        public async Task<List<ApplicationUser>> GetUsersAll()
        {
            return await context.ApplicationsUsers.ToListAsync();
        }
    }
}
