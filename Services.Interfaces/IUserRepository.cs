using DataLayer.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<ApplicationUser>> GetUsersAll();
        public ValueTask<ApplicationUser> GetUser(string id,bool IncludeTickets = false);
    }
}
